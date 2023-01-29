using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema;

namespace Tests.ApplicationName.QA.Api.TestData.YahooFinance
{
    public class TestDataYahooFinanceQuotes
    {
        public static QuoteEntityModel NasdaqCompositeDaily20220711 = new()
        {
            Date = new DateTime(2022, 07, 11),
            Open = 11524.490234m,
            High = 11541.099609m,
            Low = 11348.059570m,
            Close = 11372.599609m,
            AdjClose = 11372.599609m,
            Volume = 4343130000
        };

        public static QuoteEntityModel NasdaqCompositeDaily20220712 = new()
        {
            Date = new DateTime(2022, 07, 12),
            Open = 11420.889648m,
            High = 11483.169922m,
            Low = 11207.080078m,
            Close = 11264.730469m,
            AdjClose = 11264.730469m,
            Volume = 4279920000
        };

        public static QuoteEntityModel GenerateRandomNasdaqCompositeQuote(DateTime? dateTime = null)
        {
            dateTime ??= DateTime.UtcNow;

            return new QuoteEntityModel()
            {
                Date = dateTime.Value,
                Open = GetGeneratedNasdaqCompositeQuotePriceValue(),
                High = GetGeneratedNasdaqCompositeQuotePriceValue(),
                Low = GetGeneratedNasdaqCompositeQuotePriceValue(),
                Close = GetGeneratedNasdaqCompositeQuotePriceValue(),
                AdjClose = GetGeneratedNasdaqCompositeQuotePriceValue(),
                Volume = GetGeneratedNasdaqCompositeQuoteVolumeValue()
            };
        }

        private static decimal GetGeneratedNasdaqCompositeQuotePriceValue() => Convert.ToDecimal(DateTime.UtcNow.ToString("yyyyMMddHH,mmssfffff"));
        private static long GetGeneratedNasdaqCompositeQuoteVolumeValue() => Convert.ToInt64(DateTime.UtcNow.ToString("yyyyssffff"));
    }
}
