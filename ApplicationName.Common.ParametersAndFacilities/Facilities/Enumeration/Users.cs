using ApplicationName.ModelsAndFacilities.Models.OperationalModels.AuthModels;

namespace ApplicationName.Common.ParametersAndFacilities.Facilities.Enumeration
{
    public class Users
    {
        public static readonly AppUserModel IAmUser = new AppUserModel() { UserName = "IAmUserName", Password = "IAmUserPassword" }; //DUMMY USER - DO NOT STORE REAL CREDENTIALS IN REPOSITORY
        public static readonly AppUserModel DbUser = new AppUserModel() { UserName = "SomeUser", Password = "S0mePa33w0rd" }; //DUMMY USER - DO NOT STORE REAL CREDENTIALS IN REPOSITORY
    }
}
