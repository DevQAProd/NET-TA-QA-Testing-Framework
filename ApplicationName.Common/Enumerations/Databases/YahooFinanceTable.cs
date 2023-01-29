using System.ComponentModel;

namespace ApplicationName.Common.Enumerations.Databases
{
    public enum YahooFinanceTable
    {
        [Description("__efmigrationshistory")]
        EfMigrationsHistory,

        [Description("nasdaq_composite_daily")]
        NasdaqCompositeDaily,

        [Description("nasdaq_composite_weekly")]
        NasdaqCompositeWeekly,

        [Description("nasdaq_composite_monthly")]
        NasdaqCompositeMonthly,

        [Description("dax_performance_index_daily")]
        DaxPerformanceIndexDaily,

        [Description("dax_performance_index_weekly")]
        DaxPerformanceIndexWeekly,

        [Description("dax_performance_index_monthly")]
        DaxPerformanceIndexMonthly
    }
}
