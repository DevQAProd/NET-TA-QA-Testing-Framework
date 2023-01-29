using System.Net;
using ApplicationName.Common.Enumerations.Databases;
using ApplicationName.Databases.MySql.AppDatabase.WhoIsWhoSchema.Configurations;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.WhoIsWhoSchema;
using Microsoft.EntityFrameworkCore;

namespace ApplicationName.Databases.MySql.AppDatabase.WhoIsWhoSchema
{
    public class WhoIsWhoAppDatabaseDbContext : AppDatabaseBaseDbContext<WhoIsWhoAppDatabaseDbContext>
    {
        public DbSet<UserPasswordEntityModel> UsersPasswordsTable { get; set; }
        public DbSet<UserXApiKeyEntityModel> UsersXApiKeysTable { get; set; }

        public WhoIsWhoAppDatabaseDbContext() : base(AppDatabaseSchemaName.WhoIsWho)
        {
        }

        public WhoIsWhoAppDatabaseDbContext(NetworkCredential credentials) : base(credentials, AppDatabaseSchemaName.WhoIsWho)
        {
        }

        public WhoIsWhoAppDatabaseDbContext(string connectionString) : base(connectionString, AppDatabaseSchemaName.WhoIsWho)
        {
        }

        public WhoIsWhoAppDatabaseDbContext(DbContextOptions<WhoIsWhoAppDatabaseDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UsersPasswordsConfiguration());
            builder.ApplyConfiguration(new UsersXApiKeysConfiguration());
        }
    }
}