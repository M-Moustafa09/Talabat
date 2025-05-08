using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Talabat.Repository.Data;
using Talabat.Repository.Dbcontext;

namespace Talabat.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Configure services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StorDbcontrext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"

                    ));
            }

            );

            #endregion

            var app = builder.Build();

            #region Update-Database
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory= services.GetRequiredService<ILoggerFactory>();
            try
            {
            var dbcontext= services.GetRequiredService<StorDbcontrext>();
            await dbcontext.Database.MigrateAsync();
               await StorContextSeed.SeedAsynk(dbcontext);

            }
            catch ( Exception ex )
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Title");

            }

            #endregion

            #region Configure-Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
