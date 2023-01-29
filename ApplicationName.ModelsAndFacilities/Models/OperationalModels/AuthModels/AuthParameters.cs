using System.Net;
using ApplicationName.ModelsAndFacilities.Facilities.Interfaces;

namespace ApplicationName.ModelsAndFacilities.Models.OperationalModels.AuthModels
{
    public class AuthParameters : IAuthParameters
    {
        public NetworkCredential Credentials { get; set; }
        public Dictionary<string, object> Data { get; set; }

        public AuthParameters(NetworkCredential credentials = null, Dictionary<string, object> data = null)
        {
            Credentials = credentials;
            Data = data;
        }
    }
}
