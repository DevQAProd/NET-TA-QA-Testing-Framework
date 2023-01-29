using ApplicationName.ModelsAndFacilities.Models.OperationalModels.ApiModels;
using Microsoft.Extensions.Primitives;

namespace ApplicationName.ModelsAndFacilities.Facilities.Interfaces
{
    public interface IHttpAuthParameters
    {
        public List<Header> Headers { get; set; }
        public Cookies Cookies { get; set; }
        public Dictionary<string, StringValues> QueryStringKeysValues { get; set; }
        public void ClearHttpAuthParameters();
    }
}
