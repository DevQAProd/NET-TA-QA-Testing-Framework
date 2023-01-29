using ApplicationName.ModelsAndFacilities.Facilities.Builders;

namespace ApplicationName.ModelsAndFacilities.Models.OperationalModels.ApiModels
{
    public class HttpEndpoint
    {
        public AppUriBuilder RequestUri { get; set; }
        public HttpMethod Method { get; set; }
        public IEnumerable<Header> RequestHeaders { get; set; }
        public Cookies RequestCookies { get; set; }
        public HttpRequestOptions RequestOptions { get; set; }
    }
}
