using ApplicationName.ModelsAndFacilities.Models.ApiModels;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema;

namespace ApplicationName.ModelsAndFacilities.Facilities.Mappers.ApiModels
{
    public static class QuoteApiModelMappers
    {
        public static QuoteEntityModel ToQuoteEntityModel(this QuoteApiModel model)
        {
            return new QuoteEntityModel()
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
