using ApplicationName.ModelsAndFacilities.Models.OperationalModels.HelperModels;

namespace ApplicationName.Dev.Api.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<AuthValidationResultModel> IsPasswordValid(string userNameForValidation, string passwordForValidation);
        public Task<AuthValidationResultModel> IsBasicAuthenticationValid(string authorizeHeaderValue);
        public Task<AuthValidationResultModel> IsXApiKeyValid(string xApiKeyForValidation);
    }
}
