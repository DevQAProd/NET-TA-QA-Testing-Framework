using ApplicationName.ModelsAndFacilities.Models.EntityModels.WhoIsWhoSchema;

namespace ApplicationName.Dev.Api.Interfaces
{
    public interface IUsersPasswordsRepository
    {
        public Task<UserPasswordEntityModel> GetUserPasswordData(string userName, string password);
    }
}
