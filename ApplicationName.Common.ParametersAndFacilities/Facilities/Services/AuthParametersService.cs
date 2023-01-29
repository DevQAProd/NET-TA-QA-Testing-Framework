using System.Collections.Concurrent;
using System.Net;
using ApplicationName.Common.ParametersAndFacilities.Facilities.Enumeration;
using ApplicationName.ModelsAndFacilities.Facilities.Interfaces;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.AuthModels;

namespace ApplicationName.Common.ParametersAndFacilities.Facilities.Services
{
    public class AuthParametersService
    {
        private ConcurrentDictionary<string, IAuthParameters> AuthParameters;

        public AuthParametersService()
        {
            AuthParameters = new();
            AuthParameters.TryAdd(Users.IAmUser.UserName, new AuthParameters() { Credentials = new NetworkCredential(Users.IAmUser.UserName, Users.IAmUser.Password) });
        }

        public IAuthParameters GetAuthParametersByUserName(string userName)
        {
            if (AuthParameters.TryGetValue(userName, out var parameters))
                return parameters;
            throw new KeyNotFoundException($"No auth parameters for user with username '{userName}' were found.");
        }
    }
}
