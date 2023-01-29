using System.Net;
using ApplicationName.App.Api.Endpoints;
using ApplicationName.Core.Api.Extensions;
using ApplicationName.ModelsAndFacilities.Facilities.Builders;
using ApplicationName.ModelsAndFacilities.Models.ApiModels;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.ApiModels.HttpResponseModels;

namespace ApplicationName.App.Api.Services
{
    public class WhoIsWhoApiServiceV1
    {
        public JsonHttpResponseModel<UserXApiKeyApiModel> GetXApiKey(string userName, string password, HttpStatusCode? statusCode = HttpStatusCode.OK)
        {
            var endpoint = WhoIsWhoEndpointsV1.GetXApiKey(userName, password);

            return new HttpRequestMessageBuilder(endpoint)
                .Build()
                .Send()
                .EnsureStatusCode(statusCode)
                .ToJsonHttpResponseModel<UserXApiKeyApiModel>();
        }
    }
}
