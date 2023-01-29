using System.Net;
using System.Net.Http.Json;
using ApplicationName.App.Api.AuthenticationProcedures;
using ApplicationName.App.Api.Endpoints;
using ApplicationName.Common.Enumerations.YahooFinance;
using ApplicationName.Core.Api.AuthenticationProcedures;
using ApplicationName.Core.Api.Extensions;
using ApplicationName.ModelsAndFacilities.Facilities.Builders;
using ApplicationName.ModelsAndFacilities.Facilities.Interfaces;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.ApiModels.HttpResponseModels;

namespace ApplicationName.App.Api.Services
{
    public class YahooFinanceWithAuthenticationApiServiceV1
    {
        public JsonHttpResponseModel<List<QuoteEntityModel>> GetQuoteRecordsByDateRange(IAuthParameters authParameters, Quote quote, Frequency frequency, DateTime? from = null, DateTime? to = null, HttpStatusCode? statusCode = HttpStatusCode.OK)
        {
            var endpoint = YahooFinanceWithAuthenticationEndpointsV1.GetQuoteRecordsByDateRange(quote, frequency, from, to);
            var httpAuthenticationProcedure = new BasicHttpAuthenticationProcedure();

            return new HttpRequestMessageBuilder(endpoint)
                .WithAuthentication(httpAuthenticationProcedure, authParameters)
                .Build()
                .Send()
                .EnsureStatusCode(statusCode)
                .ToJsonHttpResponseModel<List<QuoteEntityModel>>();
        }

        public JsonHttpResponseModel<int> UpsertQuoteRecords(IAuthParameters authParameters, Quote quote, Frequency frequency, List<QuoteEntityModel> models, HttpStatusCode? statusCode = HttpStatusCode.OK)
        {
            var endpoint = YahooFinanceWithAuthenticationEndpointsV1.PostQuoteRecords(quote, frequency);
            var httpAuthenticationProcedure = new XApiKeyHttpAuthenticationProcedureWithQueryString();

            return new HttpRequestMessageBuilder(endpoint)
                .WithAuthentication(httpAuthenticationProcedure, authParameters)
                .WithHttpContent(JsonContent.Create(models))
                .Build()
                .Send()
                .EnsureStatusCode(statusCode)
                .ToJsonHttpResponseModel<int>();
        }

        public JsonHttpResponseModel<int> DeleteQuoteRecords(IAuthParameters authParameters, Quote quote, Frequency frequency, List<DateTime> dateTimeValues, HttpStatusCode? statusCode = HttpStatusCode.OK)
        {
            var endpoint = YahooFinanceWithAuthenticationEndpointsV1.DeleteQuoteRecords(quote, frequency);
            var httpAuthenticationProcedure = new XApiKeyHttpAuthenticationProcedureWithCookie();

            return new HttpRequestMessageBuilder(endpoint)
                .WithAuthentication(httpAuthenticationProcedure, authParameters)
                .WithHttpContent(JsonContent.Create(dateTimeValues))
                .Build()
                .Send()
                .EnsureStatusCode(statusCode)
                .ToJsonHttpResponseModel<int>();
        }
    }
}
