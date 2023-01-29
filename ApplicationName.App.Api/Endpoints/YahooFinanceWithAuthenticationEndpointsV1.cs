using ApplicationName.Common.Enumerations.YahooFinance;
using ApplicationName.Common.Extensions;
using ApplicationName.ModelsAndFacilities.Facilities.Builders;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.ApiModels;

namespace ApplicationName.App.Api.Endpoints
{
    public static class YahooFinanceWithAuthenticationEndpointsV1
    {
        public static HttpEndpoint GetQuoteRecordsByDateRange(Quote quote, Frequency frequency, DateTime? from, DateTime? to) => new()
        {
            Method = HttpMethod.Get,
            RequestUri = new AppUriBuilder()
                .WithScheme(Uri.UriSchemeHttps)
                .WithHost("localhost")
                .WithPort(5001)
                .WithPath("api/v1/YahooFinanceWithAuthentication/{quote}/{frequency}")
                .WithPathParameter("quote", quote.ToString())
                .WithPathParameter("frequency", frequency.ToString())
                .WithQueryStringKeyValues("from", from?.ToMySqlDbDateTimeString())
                .WithQueryStringKeyValues("to", to?.ToMySqlDbDateTimeString())
        };

        public static HttpEndpoint PostQuoteRecords(Quote quote, Frequency frequency) => new()
        {
            Method = HttpMethod.Post,
            RequestUri = new AppUriBuilder()
                .WithScheme(Uri.UriSchemeHttps)
                .WithHost("localhost")
                .WithPort(5001)
                .WithPath("api/v1/YahooFinanceWithAuthentication/{quote}/{frequency}")
                .WithPathParameter("quote", quote.ToString())
                .WithPathParameter("frequency", frequency.ToString())
        };

        public static HttpEndpoint DeleteQuoteRecords(Quote quote, Frequency frequency) => new()
        {
            Method = HttpMethod.Delete,
            RequestUri = new AppUriBuilder()
                .WithScheme(Uri.UriSchemeHttps)
                .WithHost("localhost")
                .WithPort(5001)
                .WithPath("api/v1/YahooFinanceWithAuthentication/{quote}/{frequency}")
                .WithPathParameter("quote", quote.ToString())
                .WithPathParameter("frequency", frequency.ToString())
        };
    }
}
