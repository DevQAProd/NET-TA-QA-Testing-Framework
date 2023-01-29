using ApplicationName.ModelsAndFacilities.Models.ApiModels;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.WhoIsWhoSchema;

namespace ApplicationName.ModelsAndFacilities.Facilities.Mappers.EntityModels
{
    public static class UserXApiKeyEntityModelMappers
    {
        public static UserXApiKeyApiModel ToUserXApiKeyApiModel(this UserXApiKeyEntityModel model)
        {
            return new UserXApiKeyApiModel
            {
                UserName = model.UserName,
                XApiKey = model.XApiKey,
                ValidFrom = model.ValidFrom,
                ValidTo = model.ValidTo
            };
        }
    }
}
