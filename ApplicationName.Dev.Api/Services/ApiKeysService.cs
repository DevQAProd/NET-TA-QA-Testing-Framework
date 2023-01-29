using System.Security.Cryptography;
using ApplicationName.Dev.Api.Interfaces;

namespace ApplicationName.Dev.Api.Services
{
    public class ApiKeysService : IApiKeysService
    {
        public string GenerateXApiKey()
        {
            var xApiKey = new byte[512];

            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(xApiKey);
            }

            return Convert.ToBase64String(xApiKey);
        }
    }
}
