using System.Net;

namespace ApplicationName.ModelsAndFacilities.Facilities.Interfaces
{
    public interface IAuthParameters
    {
        NetworkCredential Credentials { get; set; }
        Dictionary<string, object> Data { get; set; }
    }
}
