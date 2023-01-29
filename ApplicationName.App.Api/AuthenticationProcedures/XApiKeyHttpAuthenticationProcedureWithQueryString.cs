using ApplicationName.App.Api.Services;
using ApplicationName.Common.Constants;
using ApplicationName.Common.Extensions;
using ApplicationName.ModelsAndFacilities.Facilities.Interfaces;
using ApplicationName.ModelsAndFacilities.Models.ApiModels;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.ApiModels;
using Microsoft.Extensions.Primitives;

namespace ApplicationName.App.Api.AuthenticationProcedures
{
    public class XApiKeyHttpAuthenticationProcedureWithQueryString : IHttpAuthenticationProcedure
    {
        public IHttpAuthParameters GetHttpAuthenticationParameters(IAuthParameters authParameters)
        {
            var currentDateTime = DateTime.UtcNow;
            TimeSpan reauthenticationBoundaryTimespan = Constants.ReauthenticationBoundaryTimespan;

            if (authParameters?.Data != null)
            {
                if (authParameters.Data.TryGetValue(Constants.ReauthenticationBoundaryName, out var value) && value is TimeSpan)
                    reauthenticationBoundaryTimespan = (TimeSpan)value;

                if (authParameters.Data.TryGetValue(Constants.XApiKeyName, out var model) && model is UserXApiKeyApiModel)
                {
                    var userXApiKeyModel = model as UserXApiKeyApiModel;

                    if (currentDateTime >= userXApiKeyModel.ValidFrom && currentDateTime.AddTicks(reauthenticationBoundaryTimespan.Ticks) <= userXApiKeyModel.ValidTo)
                        return new HttpAuthParameters() { QueryStringKeysValues = new Dictionary<string, StringValues>() { { Constants.XApiKeyName, userXApiKeyModel.XApiKey } } };
                }
            }

            return GetNewXApiKey(authParameters);
        }

        private IHttpAuthParameters GetNewXApiKey(IAuthParameters authParameters)
        {
            if (string.IsNullOrEmpty(authParameters?.Credentials?.UserName) || string.IsNullOrEmpty(authParameters?.Credentials?.Password))
                throw new ArgumentException("UserName and Password should not be null or empty.");

            UserXApiKeyApiModel newXApiKeyModel = new WhoIsWhoApiServiceV1().GetXApiKey(authParameters.Credentials.UserName, authParameters.Credentials.Password).TContent;

            authParameters.Data ??= new Dictionary<string, object>();
            authParameters.Data.AddOrReplace(Constants.XApiKeyName, newXApiKeyModel);

            return new HttpAuthParameters() { QueryStringKeysValues = new Dictionary<string, StringValues>() { { Constants.XApiKeyName, newXApiKeyModel.XApiKey } } };
        }
    }
}
