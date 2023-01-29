using ApplicationName.Databases.MySql.AppDatabase.WhoIsWhoSchema;
using ApplicationName.Dev.Api.Interfaces;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.WhoIsWhoSchema;
using Microsoft.EntityFrameworkCore;

namespace ApplicationName.Dev.Api.Repositories
{
    public class UsersPasswordsRepository : IUsersPasswordsRepository
    {
        private readonly WhoIsWhoAppDatabaseDbContext _whoIsWhoDbContext;

        public UsersPasswordsRepository(WhoIsWhoAppDatabaseDbContext whoIsWhoDbContext)
        {
            _whoIsWhoDbContext = whoIsWhoDbContext;
        }

        public async Task<UserPasswordEntityModel> GetUserPasswordData(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return null;

            try
            {
                return await _whoIsWhoDbContext.UsersPasswordsTable
                    .Where(x => x.UserName == userName && x.Password == password)
                    .FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
