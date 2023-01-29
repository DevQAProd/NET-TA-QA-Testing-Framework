using ApplicationName.Common.Enumerations.Databases;
using ApplicationName.Common.Extensions;
using ApplicationName.Common.ParametersAndFacilities.Settings;
using ApplicationName.Databases.MySql.AppDatabase.WhoIsWhoSchema;
using ApplicationName.Databases.MySql.AppDatabase.YahooFinanceSchema;
using ApplicationName.Dev.Api.Interfaces;
using ApplicationName.Dev.Api.Repositories;
using ApplicationName.Dev.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace ApplicationName.Dev.Api
{
    public class StartUp
    {
        private IConfiguration Configuration { get; }

        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSwaggerGenNewtonsoftSupport();

            var yahooFinanceDbConnectionString = BaseAppSettings.GetAppDatabaseDevConnectionString(AppDatabaseSchemaName.YahooFinance.GetDescriptionAttribute()).ToString();
            services.AddDbContext<YahooFinanceAppDatabaseDbContext>(options => options.UseMySql(yahooFinanceDbConnectionString, ServerVersion.AutoDetect(yahooFinanceDbConnectionString)));
            services.AddScoped<IQuoteRepository, QuoteRepository>();

            var whoIsWhoDbConnectionString = BaseAppSettings.GetAppDatabaseDevConnectionString(AppDatabaseSchemaName.WhoIsWho.GetDescriptionAttribute()).ToString();
            services.AddDbContext<WhoIsWhoAppDatabaseDbContext>(options => options.UseMySql(whoIsWhoDbConnectionString, ServerVersion.AutoDetect(whoIsWhoDbConnectionString)));
            services.AddScoped<IUsersPasswordsRepository, UsersPasswordsRepository>();
            services.AddScoped<IUsersXApiKeysRepository, UsersXApiKeysRepository>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IApiKeysService, ApiKeysService>();
        }

        public void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
