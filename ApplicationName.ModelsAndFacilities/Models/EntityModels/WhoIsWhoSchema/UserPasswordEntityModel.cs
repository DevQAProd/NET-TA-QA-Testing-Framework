namespace ApplicationName.ModelsAndFacilities.Models.EntityModels.WhoIsWhoSchema
{
    public class UserPasswordEntityModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
