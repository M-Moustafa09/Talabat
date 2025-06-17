using AutoMapper.Execution;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Stor.Reposiotry;
using Stor.Reposiotry.Data;
using Stor.Reposiotry.Identity;
using Store.APIs.Errors;
using Store.APIs.Extensions;
using Store.APIs.Helper;
using Store.APIs.Middlwares;
using Store.Core.Entities;
using Store.Core.Entities.Identity;
using Store.Core.Repositories;

namespace Store.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			

			#region Configuer -Add services to the container.
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<StoreContext>(Options =>
			{
				Options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
				}
			);
			builder.Services.AddDbContext<AppIdentityDbcontex>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});
			//builder.Services.AddScoped<IGenericRepository<Product>, GenaricRepository<Product>>();

			builder.Services.AddSingleton<IConnectionMultiplexer>(opition =>
	{
		var connectiom = builder.Configuration.GetConnectionString("RedisConnection");
		return ConnectionMultiplexer.Connect(connectiom);


	}


			);
			builder.Services.AddApplicationService();


			builder.Services.AddIdentityServices(builder.Configuration);  

			#endregion
			var app = builder.Build();

			#region Update-DataBase

				
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var loogerfactory = services.GetRequiredService<ILoggerFactory>();
			try
			{

			var dbcontext= services.GetRequiredService<StoreContext>();
			await dbcontext.Database.MigrateAsync();

				var IdentityDbContex = services.GetRequiredService<AppIdentityDbcontex>();
				await IdentityDbContex.Database.MigrateAsync();
				var usermanger = services.GetRequiredService<UserManager<AppUser>>();
				await AppIdentityDbContextSeed.SeeduserAsync(usermanger);
				await StoreContextSeed.SeedAsync(dbcontext);

			}
			catch (Exception ex)
			{
				var logger = loogerfactory.CreateLogger<Program>();
				logger.LogError(ex, "An Error is Occured During appling The Migration");

			}

			#endregion


			#region Configure the HTTP request pipeline
			if (app.Environment.IsDevelopment())
			{
				app.UseMiddleware<ExceptionMiddlware>();
				app.AddSwagger();
			}
			
			app.UseStatusCodePagesWithReExecute("/error/{0}");
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers(); 
			#endregion

			app.Run();
		}
	}
}
