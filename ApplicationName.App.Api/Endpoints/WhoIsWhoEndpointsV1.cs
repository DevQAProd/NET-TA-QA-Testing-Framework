using ApplicationName.ModelsAndFacilities.Facilities.Builders;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.ApiModels;

namespace ApplicationName.App.Api.Endpoints
{
    public static class WhoIsWhoEndpointsV1
    {
        public static HttpEndpoint GetXApiKey(string userName, string password) => new()
        {
            Method = HttpMethod.Get,
            RequestUri = new AppUriBuilder().
                WithScheme(Uri.UriSchemeHttps)
                .WithHost("localhost")
                .WithPort(5001)
                .WithPath("api/v1/WhoIsWho/XApiKey")
                .WithQueryStringKeyValues("userName", userName)
                .WithQueryStringKeyValues("password", password)
        };
    }
}
