namespace ApplicationName.ModelsAndFacilities.Models.OperationalModels.HelperModels
{
    public class ResultModel<T>
    {
        public T Result { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }

        public ResultModel(T result, Exception exception = null, string message = null)
        {
            Result = result;
            Message = message;
            Exception = exception;
        }
    }
}