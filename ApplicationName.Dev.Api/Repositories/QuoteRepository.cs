using ApplicationName.Common.Enumerations.YahooFinance;
using ApplicationName.Common.Extensions;
using ApplicationName.Databases.MySql.AppDatabase.YahooFinanceSchema;
using ApplicationName.Dev.Api.Interfaces;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema.DaxPerformanceIndex;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema.NasdaqComposite;
using Microsoft.EntityFrameworkCore;

namespace ApplicationName.Dev.Api.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly YahooFinanceAppDatabaseDbContext _yfDbContext;

        public QuoteRepository(YahooFinanceAppDatabaseDbContext yfDbContext)
        {
            _yfDbContext = yfDbContext;
        }

        public async Task<IEnumerable<QuoteEntityModel>> GetQuoteRecordsByDateRange(Quote quote, Frequency frequency, DateTime? from, DateTime? to)
        {
            try
            {
                var dbSet = _yfDbContext.GetDbSet(quote, frequency);
                return await dbSet.Where(x => x.Date >= from && x.Date <= to).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> UpsertQuoteRecords(Quote quote, Frequency frequency, params QuoteEntityModel[] quoteEntityModels)
        {
            if (quoteEntityModels?.Length > 0)
            {
                try
                {
                    var dbSet = _yfDbContext.GetDbSet(quote, frequency);

                    IEnumerable<DateTime> upsertQuotesDateValues = quoteEntityModels.Select(x => x.Date.ToMySqlDbDateTime());
                    IQueryable<DateTime> existingQuotesDateValues = dbSet.Select(x => x.Date).Where(x => upsertQuotesDateValues.Contains(x));

                    IEnumerable<QuoteEntityModel> quotesToUpdate = quoteEntityModels.Where(x => existingQuotesDateValues.Contains(x.Date));
                    IEnumerable<QuoteEntityModel> quotesToAdd = quoteEntityModels.Except(quotesToUpdate);

                    AddQuoteRecordsToDbContextWithoutSaveToDb(quote, frequency, quotesToAdd.ToArray());
                    UpdateQuoteRecordsInDbContextWithoutSaveToDb(quote, frequency, quotesToUpdate.ToArray());
                    int affectedRecordsCount = await _yfDbContext.SaveChangesAsync();

                    return affectedRecordsCount;
                }
                catch
                {
                    return -1;
                }
            }

            return 0;
        }

        public async Task<int> DeleteQuoteRecords(Quote quote, Frequency frequency, params DateTime[] dateTimeValues)
        {
            if (dateTimeValues?.Length > 0)
            {
                try
                {
                    var dbSet = _yfDbContext.GetDbSet(quote, frequency);

                    var dateTimeValuesList = dateTimeValues.ToList();
                    var removedItems = dbSet.Where(x => dateTimeValuesList.Contains(x.Date));

                    _yfDbContext.RemoveRange(removedItems);
                    int affectedRecordsCount = await _yfDbContext.SaveChangesAsync();

                    return affectedRecordsCount;
                }
                catch
                {
                    return -1;
                }
            }

            return 0;
        }

        private void AddQuoteRecordsToDbContextWithoutSaveToDb(Quote quote, Frequency frequency, params QuoteEntityModel[] quoteEntityModels)
        {
            if (quoteEntityModels?.Length > 0)
            {
                switch (quote, frequency)
                {
                    case (Quote.NasdaqComposite, Frequency.Daily):
                        _yfDbContext.AddRange(quoteEntityModels.Select(x => x.CloneJson<NasdaqCompositeDailyEntityModel>()));
                        break;
                    case (Quote.NasdaqComposite, Frequency.Weekly):
                        _yfDbContext.AddRange(quoteEntityModels.Select(x => x.CloneJson<NasdaqCompositeWeeklyEntityModel>()));
                        break;
                    case (Quote.NasdaqComposite, Frequency.Monthly):
                        _yfDbContext.AddRange(quoteEntityModels.Select(x => x.CloneJson<NasdaqCompositeMonthlyEntityModel>()));
                        break;
                    case (Quote.DaxPerformanceIndex, Frequency.Daily):
                        _yfDbContext.AddRange(quoteEntityModels.Select(x => x.CloneJson<DaxPerformanceIndexDailyEntityModel>()));
                        break;
                    case (Quote.DaxPerformanceIndex, Frequency.Weekly):
                        _yfDbContext.AddRange(quoteEntityModels.Select(x => x.CloneJson<DaxPerformanceIndexWeeklyEntityModel>()));
                        break;
                    case (Quote.DaxPerformanceIndex, Frequency.Monthly):
                        _yfDbContext.AddRange(quoteEntityModels.Select(x => x.CloneJson<DaxPerformanceIndexMonthlyEntityModel>()));
                        break;
                }
            }
        }

        private void UpdateQuoteRecordsInDbContextWithoutSaveToDb(Quote quote, Frequency frequency, params QuoteEntityModel[] quoteEntityModels)
        {
            if (quoteEntityModels?.Length > 0)
            {
                switch (quote, frequency)
                {
                    case (Quote.NasdaqComposite, Frequency.Daily):
                        _yfDbContext.UpdateRange(quoteEntityModels.Select(x => x.CloneJson<NasdaqCompositeDailyEntityModel>()));
                        break;
                    case (Quote.NasdaqComposite, Frequency.Weekly):
                        _yfDbContext.UpdateRange(quoteEntityModels.Select(x => x.CloneJson<NasdaqCompositeWeeklyEntityModel>()));
                        break;
                    case (Quote.NasdaqComposite, Frequency.Monthly):
                        _yfDbContext.UpdateRange(quoteEntityModels.Select(x => x.CloneJson<NasdaqCompositeMonthlyEntityModel>()));
                        break;
                    case (Quote.DaxPerformanceIndex, Frequency.Daily):
                        _yfDbContext.UpdateRange(quoteEntityModels.Select(x => x.CloneJson<DaxPerformanceIndexDailyEntityModel>()));
                        break;
                    case (Quote.DaxPerformanceIndex, Frequency.Weekly):
                        _yfDbContext.UpdateRange(quoteEntityModels.Select(x => x.CloneJson<DaxPerformanceIndexWeeklyEntityModel>()));
                        break;
                    case (Quote.DaxPerformanceIndex, Frequency.Monthly):
                        _yfDbContext.UpdateRange(quoteEntityModels.Select(x => x.CloneJson<DaxPerformanceIndexMonthlyEntityModel>()));
                        break;
                }
            }
        }
    }
}
