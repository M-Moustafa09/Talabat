using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Stor.Reposiotry.Identity;
using Stor.Servics;
using Store.Core.Entities.Identity;
using Store.Core.Services;
using System.Text;

namespace Store.APIs.Extensions
{
	public static class IdentityServicesExtentions
	{

		public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration _configuration)
		{
			Services.AddScoped<ITokenService, TokenService>();
			Services.AddIdentity<AppUser, IdentityRole>()
										.AddEntityFrameworkStores<AppIdentityDbcontex>();
			Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opition=>
				{
					opition.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidIssuer= _configuration["JWT:ValidIssuer"],
						ValidateAudience= true,
						ValidAudience= _configuration["JWT:ValidAudience"],
						ValidateLifetime= true,
						ValidateIssuerSigningKey= true,
						IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))
				};


				});

			return Services;
		}
	}
}
