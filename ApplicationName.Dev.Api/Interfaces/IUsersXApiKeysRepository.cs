using ApplicationName.ModelsAndFacilities.Models.EntityModels.WhoIsWhoSchema;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.HelperModels;

namespace ApplicationName.Dev.Api.Interfaces
{
    public interface IUsersXApiKeysRepository
    {
        public Task<UserXApiKeyEntityModel> GetXApiKeyData(string xApiKey);
        public Task<ResultModel<int>> UpsertXApiKey(UserXApiKeyEntityModel xApiKeyModel);
    }
}
