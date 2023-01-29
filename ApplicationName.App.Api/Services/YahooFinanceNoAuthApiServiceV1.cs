using System.Net;
using System.Net.Http.Json;
using ApplicationName.App.Api.Endpoints;
using ApplicationName.Common.Enumerations.YahooFinance;
using ApplicationName.Core.Api.Extensions;
using ApplicationName.ModelsAndFacilities.Facilities.Builders;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.ApiModels.HttpResponseModels;

namespace ApplicationName.App.Api.Services
{
    public class YahooFinanceNoAuthApiServiceV1
    {
        public JsonHttpResponseModel<List<QuoteEntityModel>> GetQuoteRecordsByDateRange(Quote quote, Frequency frequency, DateTime? from = null, DateTime? to = null, HttpStatusCode? statusCode = HttpStatusCode.OK)
        {
            var endpoint = YahooFinanceNoAuthEndpointsV1.GetQuoteRecordsByDateRange(quote, frequency, from, to);

            return new HttpRequestMessageBuilder(endpoint)
                .Build()
                .Send()
                .EnsureStatusCode(statusCode)
                .ToJsonHttpResponseModel<List<QuoteEntityModel>>();
        }

        public JsonHttpResponseModel<int> UpsertQuoteRecords(Quote quote, Frequency frequency, List<QuoteEntityModel> models, HttpStatusCode? statusCode = HttpStatusCode.OK)
        {
            var endpoint = YahooFinanceNoAuthEndpointsV1.PostQuoteRecords(quote, frequency);

            return new HttpRequestMessageBuilder(endpoint)
                .WithHttpContent(JsonContent.Create(models))
                .Build()
                .Send()
                .EnsureStatusCode(statusCode)
                .ToJsonHttpResponseModel<int>();
        }

        public JsonHttpResponseModel<int> DeleteQuoteRecords(Quote quote, Frequency frequency, List<DateTime> dateTimeValues, HttpStatusCode? statusCode = HttpStatusCode.OK)
        {
            var endpoint = YahooFinanceNoAuthEndpointsV1.DeleteQuoteRecords(quote, frequency);

            return new HttpRequestMessageBuilder(endpoint)
                .WithHttpContent(JsonContent.Create(dateTimeValues))
                .Build()
                .Send()
                .EnsureStatusCode(statusCode)
                .ToJsonHttpResponseModel<int>();
        }
    }
}
