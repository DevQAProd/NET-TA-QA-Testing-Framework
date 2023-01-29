namespace ApplicationName.ModelsAndFacilities.Models.OperationalModels.HelperModels
{
    public class AuthValidationResultModel
    {
        public bool IsAuthValid { get; set; }
        public string ErrorMessage { get; set; }

        public AuthValidationResultModel(bool isAuthValid, string errorMessage = null)
        {
            IsAuthValid = isAuthValid;
            ErrorMessage = errorMessage;
        }
    }
}
