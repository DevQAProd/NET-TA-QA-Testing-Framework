namespace ApplicationName.ModelsAndFacilities.Models.ApiModels
{
    public class UserXApiKeyApiModel
    {
        public string UserName { get; set; }

        public string XApiKey { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
