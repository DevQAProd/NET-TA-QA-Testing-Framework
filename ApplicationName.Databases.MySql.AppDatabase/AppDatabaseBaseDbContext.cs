using System.Net;
using ApplicationName.Common.Enumerations.Databases;
using ApplicationName.Common.Extensions;
using ApplicationName.Common.ParametersAndFacilities.Settings;
using Microsoft.EntityFrameworkCore;

namespace ApplicationName.Databases.MySql.AppDatabase
{
    public abstract class AppDatabaseBaseDbContext<T> : DbContext where T : DbContext
    {
        public AppDatabaseSchemaName SchemaName { get; }
        public NetworkCredential Credentials { get; set; }
        public string ConnectionString { get; set; }

        protected AppDatabaseBaseDbContext(AppDatabaseSchemaName schemaName)
        {
            SchemaName = schemaName;
        }

        protected AppDatabaseBaseDbContext(NetworkCredential credentials, AppDatabaseSchemaName schemaName) : this(schemaName)
        {
            Credentials = credentials;
        }

        protected AppDatabaseBaseDbContext(string connectionString, AppDatabaseSchemaName schemaName) : this(schemaName)
        {
            ConnectionString = connectionString;
        }

        protected AppDatabaseBaseDbContext(DbContextOptions<T> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                optionsBuilder.UseMySql(ConnectionString, BaseAppSettings.MySqlServerVersion);
            }
            else if (Credentials != null && !string.IsNullOrEmpty(Credentials.UserName) && !string.IsNullOrEmpty(Credentials.Password))
            {
                var connectionString = BaseAppSettings.GetAppDatabaseConnectionString(Credentials, SchemaName.GetDescriptionAttribute()).ToString();
                optionsBuilder.UseMySql(connectionString, BaseAppSettings.MySqlServerVersion);
            }
            else
            {
                optionsBuilder.UseMySql(BaseAppSettings.MySqlServerVersion);
            }
        }
    }
}
