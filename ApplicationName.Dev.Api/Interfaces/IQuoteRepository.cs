using ApplicationName.Common.Enumerations.YahooFinance;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema;

namespace ApplicationName.Dev.Api.Interfaces
{
    public interface IQuoteRepository
    {
        Task<IEnumerable<QuoteEntityModel>> GetQuoteRecordsByDateRange(Quote quote, Frequency frequency, DateTime? from, DateTime? to);
        Task<int> UpsertQuoteRecords(Quote quote, Frequency frequency, params QuoteEntityModel[] quoteEntityModels);
        Task<int> DeleteQuoteRecords(Quote quote, Frequency frequency, params DateTime[] dateTimeValues);
    }
}
