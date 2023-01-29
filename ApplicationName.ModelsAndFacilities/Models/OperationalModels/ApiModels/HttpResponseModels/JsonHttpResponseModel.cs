namespace ApplicationName.ModelsAndFacilities.Models.OperationalModels.ApiModels.HttpResponseModels
{
    public class JsonHttpResponseModel<T> : TextHttpResponseModel
    {
        public T TContent { get; set; }
    }
}
