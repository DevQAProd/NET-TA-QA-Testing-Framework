using System.Net;
using ApplicationName.Common.Enumerations.YahooFinance;
using ApplicationName.Dev.Api.Attributes;
using ApplicationName.Dev.Api.Interfaces;
using ApplicationName.ModelsAndFacilities.Facilities.Mappers.ApiModels;
using ApplicationName.ModelsAndFacilities.Facilities.Mappers.EntityModels;
using ApplicationName.ModelsAndFacilities.Models.ApiModels;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationName.Dev.Api.Controllers.YahooFinance.v1
{
    [Route("api/v1/YahooFinanceWithAuthentication")]
    [ApiController]
    public class YahooFinanceControllerWithAuthenticationV1 : ControllerBase
    {
        private readonly IQuoteRepository _quoteRepository;

        public YahooFinanceControllerWithAuthenticationV1(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        [HttpGet("{quote}/{frequency}")]
        [Produces("application/json")]
        [BasicAuthenticationWithHeader]
        public async Task<ActionResult<IEnumerable<QuoteApiModel>>> GetQuoteDataByDateRange(Quote quote, Frequency frequency, DateTime? from, DateTime? to)
        {
            to ??= DateTime.UtcNow;
            from ??= to.Value.AddDays(-100);

            IEnumerable<QuoteApiModel> result = null;

            try
            {
                IEnumerable<QuoteEntityModel> quoteEntityModels = await _quoteRepository.GetQuoteRecordsByDateRange(quote, frequency, from, to);
                result = quoteEntityModels?.Select(x => x.ToQuoteApiModel());
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.NotImplemented, "Exception was thrown. Unable to retrieve requested data from the database.");
            }

            return result != null ? Ok(result) : StatusCode((int)HttpStatusCode.NotImplemented, "Exception was thrown. Unable to retrieve requested data from the database.");
        }

        [HttpPost("{quote}/{frequency}")]
        [HttpPut("{quote}/{frequency}")]
        [XApiKeyAuthenticationWithQueryString]
        public async Task<ActionResult<int>> PostOrPutQuoteData(Quote quote, Frequency frequency, [FromBody] QuoteApiModel[] quotes)
        {
            var affectedRecordsCount = int.MinValue;

            if (!ModelState.IsValid)
                return BadRequest("Incorrect models of Quotes were provided in the Body of the Request.");

            if (quotes?.Length > 0)
            {
                try
                {
                    affectedRecordsCount = await _quoteRepository.UpsertQuoteRecords(quote, frequency, quotes.Select(x => x.ToQuoteEntityModel()).ToArray());
                }
                catch
                {
                    return StatusCode((int)HttpStatusCode.NotImplemented, "Exception was thrown. Unable to insert/update provided data.");
                }

                return (affectedRecordsCount == quotes?.Length) ? Ok(affectedRecordsCount) : StatusCode((int)HttpStatusCode.NotImplemented, "Exception was thrown. Unable to insert/update provided data.");
            }

            return StatusCode((int)HttpStatusCode.BadRequest, "No Quotes were provided in the Body of the Request for the Insert and/or Update.");
        }

        [HttpDelete("{quote}/{frequency}")]
        [XApiKeyAuthenticationWithCookie]
        public async Task<ActionResult<int>> DeleteQuoteData(Quote quote, Frequency frequency, [FromBody] DateTime[] dateTimeValues)
        {
            if (!ModelState.IsValid)
                return BadRequest("Incorrect models of DateTime values were provided in the Body of the Request.");

            if (dateTimeValues?.Length > 0)
            {
                var affectedRecordsCount = int.MinValue;

                try
                {
                    affectedRecordsCount = await _quoteRepository.DeleteQuoteRecords(quote, frequency, dateTimeValues);
                }
                catch
                {
                    return StatusCode((int)HttpStatusCode.NotImplemented, "Exception was thrown. Unable to delete data.");
                }

                if (affectedRecordsCount > -1)
                    return Ok(affectedRecordsCount);
                return StatusCode((int)HttpStatusCode.NotImplemented, "Exception was thrown. Unable to delete data.");
            }

            return StatusCode((int)HttpStatusCode.BadRequest, "No DateTime values were provided in the Body of the Request for Deletion.");
        }
    }
}
