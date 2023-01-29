namespace ApplicationName.ModelsAndFacilities.Models.EntityModels.WhoIsWhoSchema
{
    public class UserXApiKeyEntityModel
    {
        public string UserName { get; set; }
        public string XApiKey { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public virtual UserPasswordEntityModel UserPasswordEntityModel { get; set; }
    }
}
