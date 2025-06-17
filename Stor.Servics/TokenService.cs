using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Core.Entities.Identity;
using Store.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Stor.Servics
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public async Task<string> CreateTokenAsync(AppUser User, UserManager<AppUser> userManager)
		{
			var AuthClaims = new List<Claim>() 
			{
			new Claim(ClaimTypes.GivenName,User.Displayname),
			new Claim(ClaimTypes.Email,User.Email)
			};
			var UserRoles= await userManager.GetRolesAsync(User);
			foreach(var role in UserRoles)
			{
				AuthClaims.Add(new Claim(ClaimTypes.Role,role));
			}
			var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
			var Token = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDay"])),
				claims: AuthClaims,
				signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)

				);
			return new JwtSecurityTokenHandler().WriteToken(Token);
			

		}
	}
}
