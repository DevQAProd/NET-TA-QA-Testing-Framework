using ApplicationName.ModelsAndFacilities.Models.ApiModels;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema;

namespace ApplicationName.ModelsAndFacilities.Facilities.Mappers.EntityModels
{
    public static class QuoteEntityModelMappers
    {
        public static QuoteApiModel ToQuoteApiModel(this QuoteEntityModel model)
        {
            return new QuoteApiModel()
            {
                Date = model.Date,
                Open = model.Open,
                High = model.High,
                Low = model.Low,
                Close = model.Close,
                AdjClose = model.AdjClose,
                Volume = model.Volume
            };
        }
    }
}
