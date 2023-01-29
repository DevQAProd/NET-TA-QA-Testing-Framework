using System.Net;
using ApplicationName.Common.Enumerations.Databases;
using ApplicationName.Common.Enumerations.YahooFinance;
using ApplicationName.Databases.MySql.AppDatabase.DataReaders;
using ApplicationName.Databases.MySql.AppDatabase.Resources;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema.DaxPerformanceIndex;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema.NasdaqComposite;
using Microsoft.EntityFrameworkCore;

namespace ApplicationName.Databases.MySql.AppDatabase.YahooFinanceSchema
{
    public class YahooFinanceAppDatabaseDbContext : AppDatabaseBaseDbContext<YahooFinanceAppDatabaseDbContext>
    {
        public DbSet<NasdaqCompositeDailyEntityModel> NasdaqCompositeDailyTable { get; set; }
        public DbSet<NasdaqCompositeWeeklyEntityModel> NasdaqCompositeWeeklyTable { get; set; }
        public DbSet<NasdaqCompositeMonthlyEntityModel> NasdaqCompositeMonthlyTable { get; set; }

        public DbSet<DaxPerformanceIndexDailyEntityModel> DaxPerformanceIndexDailyTable { get; set; }
        public DbSet<DaxPerformanceIndexWeeklyEntityModel> DaxPerformanceIndexWeeklyTable { get; set; }
        public DbSet<DaxPerformanceIndexMonthlyEntityModel> DaxPerformanceIndexMonthlyTable { get; set; }

        public YahooFinanceAppDatabaseDbContext() : base(AppDatabaseSchemaName.YahooFinance)
        {
        }

        public YahooFinanceAppDatabaseDbContext(NetworkCredential credentials) : base(credentials, AppDatabaseSchemaName.YahooFinance)
        {
        }

        public YahooFinanceAppDatabaseDbContext(string connectionString) : base(connectionString, AppDatabaseSchemaName.YahooFinance)
        {
        }

        public YahooFinanceAppDatabaseDbContext(DbContextOptions<YahooFinanceAppDatabaseDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var dataReader = new YahooFinanceDataReader();
            char delimiter = ',';

            builder.ApplyConfiguration(new YahooFinanceTablesBaseConfiguration<NasdaqCompositeDailyEntityModel>(YahooFinanceTable.NasdaqCompositeDaily, MySqlDbColumnType.Decimal19_9));
            builder.Entity<NasdaqCompositeDailyEntityModel>().HasData(dataReader.ReadYahooFinanceQuoteData(ResourcesYahooFinance.IXIC_Daily, delimiter));

            builder.ApplyConfiguration(new YahooFinanceTablesBaseConfiguration<NasdaqCompositeWeeklyEntityModel>(YahooFinanceTable.NasdaqCompositeWeekly, MySqlDbColumnType.Decimal19_9));
            builder.Entity<NasdaqCompositeWeeklyEntityModel>().HasData(dataReader.ReadYahooFinanceQuoteData(ResourcesYahooFinance.IXIC_Weekly, delimiter));

            builder.ApplyConfiguration(new YahooFinanceTablesBaseConfiguration<NasdaqCompositeMonthlyEntityModel>(YahooFinanceTable.NasdaqCompositeMonthly, MySqlDbColumnType.Decimal19_9));
            builder.Entity<NasdaqCompositeMonthlyEntityModel>().HasData(dataReader.ReadYahooFinanceQuoteData(ResourcesYahooFinance.IXIC_Monthly, delimiter));

            builder.ApplyConfiguration(new YahooFinanceTablesBaseConfiguration<DaxPerformanceIndexDailyEntityModel>(YahooFinanceTable.DaxPerformanceIndexDaily, MySqlDbColumnType.Decimal19_9));
            builder.Entity<DaxPerformanceIndexDailyEntityModel>().HasData(dataReader.ReadYahooFinanceQuoteData(ResourcesYahooFinance.GDAXI_Daily, delimiter));

            builder.ApplyConfiguration(new YahooFinanceTablesBaseConfiguration<DaxPerformanceIndexWeeklyEntityModel>(YahooFinanceTable.DaxPerformanceIndexWeekly, MySqlDbColumnType.Decimal19_9));
            builder.Entity<DaxPerformanceIndexWeeklyEntityModel>().HasData(dataReader.ReadYahooFinanceQuoteData(ResourcesYahooFinance.GDAXI_Weekly, delimiter));

            builder.ApplyConfiguration(new YahooFinanceTablesBaseConfiguration<DaxPerformanceIndexMonthlyEntityModel>(YahooFinanceTable.DaxPerformanceIndexMonthly, MySqlDbColumnType.Decimal19_9));
            builder.Entity<DaxPerformanceIndexMonthlyEntityModel>().HasData(dataReader.ReadYahooFinanceQuoteData(ResourcesYahooFinance.GDAXI_Monthly, delimiter));
        }

        public IQueryable<QuoteEntityModel> GetDbSet(Quote quote, Frequency frequency)
        {
            switch (quote, frequency)
            {
                case (Quote.NasdaqComposite, Frequency.Daily): return NasdaqCompositeDailyTable;
                case (Quote.NasdaqComposite, Frequency.Weekly): return NasdaqCompositeWeeklyTable;
                case (Quote.NasdaqComposite, Frequency.Monthly): return NasdaqCompositeMonthlyTable;
                case (Quote.DaxPerformanceIndex, Frequency.Daily): return DaxPerformanceIndexDailyTable;
                case (Quote.DaxPerformanceIndex, Frequency.Weekly): return DaxPerformanceIndexWeeklyTable;
                case (Quote.DaxPerformanceIndex, Frequency.Monthly): return DaxPerformanceIndexMonthlyTable;
                default:
                    throw new ArgumentException($"DbSet for quote {quote} and frequency {frequency} was not found.");
            }
        }
    }
}