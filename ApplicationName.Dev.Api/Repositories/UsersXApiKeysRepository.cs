using ApplicationName.Databases.MySql.AppDatabase.WhoIsWhoSchema;
using ApplicationName.Dev.Api.Interfaces;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.WhoIsWhoSchema;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.HelperModels;
using Microsoft.EntityFrameworkCore;


namespace ApplicationName.Dev.Api.Repositories
{
    public class UsersXApiKeysRepository : IUsersXApiKeysRepository
    {
        private readonly WhoIsWhoAppDatabaseDbContext _whoIsWhoDbContext;

        public UsersXApiKeysRepository(WhoIsWhoAppDatabaseDbContext whoIsWhoDbContext)
        {
            _whoIsWhoDbContext = whoIsWhoDbContext;
        }

        public async Task<UserXApiKeyEntityModel> GetXApiKeyData(string xApiKey)
        {
            if (string.IsNullOrEmpty(xApiKey))
                return null;

            try
            {
                return await _whoIsWhoDbContext.UsersXApiKeysTable
                    .Where(x => x.XApiKey == xApiKey)
                    .FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResultModel<int>> UpsertXApiKey(UserXApiKeyEntityModel xApiKeyModel)
        {
            if (xApiKeyModel == null)
                return new ResultModel<int>(-1, new ArgumentException("'User X-API-Key' Model should not be null."));

            bool? isXApiKeyForUserExists = null;

            try
            {
                isXApiKeyForUserExists = await _whoIsWhoDbContext.UsersXApiKeysTable.AnyAsync(x => x.UserName == xApiKeyModel.UserName);
            }
            catch (Exception ex)
            {
                return new ResultModel<int>(-1, ex);
            }

            if (isXApiKeyForUserExists == true)
                _whoIsWhoDbContext.Update(xApiKeyModel);
            else
                _whoIsWhoDbContext.Add(xApiKeyModel);

            try
            {
                var result = await _whoIsWhoDbContext.SaveChangesAsync();
                return new ResultModel<int>(result);
            }
            catch (Exception ex)
            {
                return new ResultModel<int>(-1, ex);
            }
        }
    }
}
