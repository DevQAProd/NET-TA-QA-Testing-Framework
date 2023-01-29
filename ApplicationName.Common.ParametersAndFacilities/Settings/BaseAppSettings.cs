using System.Net;
using ApplicationName.Common.ParametersAndFacilities.Facilities.Enumeration;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace ApplicationName.Common.ParametersAndFacilities.Settings
{
    public partial class BaseAppSettings
    {
        public static MySqlServerVersion MySqlServerVersion = new(new Version(8, 0, 29));

        public static MySqlConnectionStringBuilder GetAppDatabaseConnectionString(NetworkCredential credentials, string schemaName = null)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder();
            connectionStringBuilder.Server = "127.0.0.1";
            connectionStringBuilder.Port = 3306;
            connectionStringBuilder.UserID = credentials?.UserName;
            connectionStringBuilder.Password = credentials?.Password;

            connectionStringBuilder.Database = schemaName;

            return connectionStringBuilder;
        }

        public static MySqlConnectionStringBuilder GetAppDatabaseDevConnectionString(string schemaName = null)
        {
            return GetAppDatabaseConnectionString(new NetworkCredential(userName: Users.DbUser.UserName, password: Users.DbUser.Password), schemaName);
        }
    }
}
