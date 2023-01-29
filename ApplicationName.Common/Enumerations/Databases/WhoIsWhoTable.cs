using System.ComponentModel;

namespace ApplicationName.Common.Enumerations.Databases
{
    public enum WhoIsWhoTable
    {
        [Description("__efmigrationshistory")]
        EfMigrationsHistory,
        [Description("users_passwords")]
        UsersPasswords,
        [Description("users_x_api_keys")]
        UsersXApiKeys
    }
}
