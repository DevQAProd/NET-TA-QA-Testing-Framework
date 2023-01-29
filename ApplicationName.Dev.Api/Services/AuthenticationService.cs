using System.Net.Http.Headers;
using System.Text;
using ApplicationName.Common.Extensions;
using ApplicationName.Common.Extensions.StringExtensions;
using ApplicationName.Dev.Api.Interfaces;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.HelperModels;

namespace ApplicationName.Dev.Api.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUsersPasswordsRepository _usersPasswordsRepository;
        private readonly IUsersXApiKeysRepository _usersXApiKeysRepository;

        public AuthenticationService(IUsersPasswordsRepository usersPasswordsRepository, IUsersXApiKeysRepository usersXApiKeysRepository)
        {
            _usersPasswordsRepository = usersPasswordsRepository;
            _usersXApiKeysRepository = usersXApiKeysRepository;
        }

        public async Task<AuthValidationResultModel> IsPasswordValid(string userNameForValidation, string passwordForValidation)
        {
            if (string.IsNullOrEmpty(userNameForValidation) && string.IsNullOrEmpty(passwordForValidation))
                return new AuthValidationResultModel(false, "Username and/or Password can not be null or empty.");

            var dbUserPasswordData = await _usersPasswordsRepository.GetUserPasswordData(userNameForValidation, passwordForValidation.ToSha512HashedBase64EncodedString());

            if (dbUserPasswordData != null)
            {
                var currentDateTime = DateTime.UtcNow;
                var currentDateTimeString = currentDateTime.ToMySqlDbDateTimeWith6FractionalPointsString();

                if (currentDateTime < dbUserPasswordData.ValidFrom)
                {
                    return new AuthValidationResultModel(false,
                        $"Current Date: '{currentDateTimeString}'. Credentials will become valid from: '{dbUserPasswordData.ValidFrom.ToMySqlDbDateTimeWith6FractionalPointsString()}'.");
                }
                else if (currentDateTime >= dbUserPasswordData.ValidFrom && currentDateTime <= dbUserPasswordData.ValidTo)
                {
                    return new AuthValidationResultModel(true);
                }
                else if (dbUserPasswordData.ValidTo < currentDateTime)
                {
                    return new AuthValidationResultModel(false,
                        $"Current Date: '{currentDateTimeString}'. Credentials have expired. They were valid up to: '{dbUserPasswordData.ValidTo.ToMySqlDbDateTimeWith6FractionalPointsString()}'.");
                }
            }

            return new AuthValidationResultModel(false, "Credentials are invalid.");
        }

        public async Task<AuthValidationResultModel> IsBasicAuthenticationValid(string authorizeHeaderValue)
        {
            if (string.IsNullOrEmpty(authorizeHeaderValue))
                return new AuthValidationResultModel(false, "No 'Authorization' Header was provided in the HTTP Request.");

            var authorizeHeaderValueParsed = AuthenticationHeaderValue.Parse(authorizeHeaderValue);

            if (authorizeHeaderValueParsed.Scheme != "Basic")
                return new AuthValidationResultModel(false, "The 'Authorization' is not of Basic type.");

            if (string.IsNullOrEmpty(authorizeHeaderValueParsed.Parameter))
                return new AuthValidationResultModel(false, "'Authorization' credentials were not provided.");

            byte[] decodedValue = Convert.FromBase64String(authorizeHeaderValueParsed.Parameter);
            string plainText = Encoding.GetEncoding("ISO-8859-1").GetString(decodedValue);
            string[] decodedParameters = plainText.Split(":");

            if (decodedParameters.Length != 2)
                return new AuthValidationResultModel(false, "Username and/or Password was/were not provided.");

            var userNameForValidation = decodedParameters.ElementAt(0);
            var passwordForValidation = decodedParameters.ElementAt(1);

            if (string.IsNullOrEmpty(userNameForValidation) && string.IsNullOrEmpty(passwordForValidation))
                return new AuthValidationResultModel(false, "Username and/or Password can not be null or empty.");

            if (string.IsNullOrEmpty(userNameForValidation))
                return new AuthValidationResultModel(false, "Username can not be null or empty.");
            if (string.IsNullOrEmpty(passwordForValidation))
                return new AuthValidationResultModel(false, "Password can not be null or empty.");

            return await IsPasswordValid(userNameForValidation, passwordForValidation);
        }

        public async Task<AuthValidationResultModel> IsXApiKeyValid(string xApiKeyForValidation)
        {
            if (string.IsNullOrEmpty(xApiKeyForValidation))
                return new AuthValidationResultModel(false, "X-API-Key is either null or empty.");

            var dbXApiKeyData = await _usersXApiKeysRepository.GetXApiKeyData(xApiKeyForValidation.ToSha512HashedBase64EncodedString());

            if (dbXApiKeyData != null)
            {
                var currentDateTime = DateTime.UtcNow;
                var currentDateTimeString = currentDateTime.ToMySqlDbDateTimeWith6FractionalPointsString();

                if (currentDateTime < dbXApiKeyData.ValidFrom)
                {
                    return new AuthValidationResultModel(false,
                        $"Current Date: '{currentDateTimeString}'. X-API-Key will become valid from: '{dbXApiKeyData.ValidFrom.ToMySqlDbDateTimeWith6FractionalPointsString()}'.");
                }
                else if (currentDateTime >= dbXApiKeyData.ValidFrom && currentDateTime <= dbXApiKeyData.ValidTo)
                {
                    return new AuthValidationResultModel(true);
                }
                else if (dbXApiKeyData.ValidTo < currentDateTime)
                {
                    return new AuthValidationResultModel(false,
                        $"Current Date: '{currentDateTimeString}'. X-API-Key has expired. It was valid up to: '{dbXApiKeyData.ValidTo.ToMySqlDbDateTimeWith6FractionalPointsString()}'.");
                }
            }

            return new AuthValidationResultModel(false, "X-API-Key is invalid.");
        }
    }
}
