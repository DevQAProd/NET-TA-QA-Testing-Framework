using System.Net;
using ApplicationName.Common.Extensions.StringExtensions;
using ApplicationName.Dev.Api.Interfaces;
using ApplicationName.ModelsAndFacilities.Facilities.Mappers.EntityModels;
using ApplicationName.ModelsAndFacilities.Models.ApiModels;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.WhoIsWhoSchema;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.HelperModels;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationName.Dev.Api.Controllers.WhoIsWho.v1
{
    [Route("api/v1/WhoIsWho")]
    [ApiController]
    public class WhoIsWhoControllerV1 : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUsersXApiKeysRepository _usersXApiKeysRepository;
        private readonly IApiKeysService _apiKeysService;

        public WhoIsWhoControllerV1(IApiKeysService apiKeysService, IAuthenticationService authenticationService, IUsersXApiKeysRepository usersXApiKeysRepository)
        {
            _apiKeysService = apiKeysService;
            _authenticationService = authenticationService;
            _usersXApiKeysRepository = usersXApiKeysRepository;
        }

        [HttpGet("XApiKey")]
        [Produces("application/json")]
        public async Task<ActionResult<UserXApiKeyApiModel>> GetXApiKey(string userName, string password)
        {
            AuthValidationResultModel authValidationResultModel = await _authenticationService.IsPasswordValid(userName, password);

            if (!authValidationResultModel.IsAuthValid)
                return Unauthorized(authValidationResultModel.ErrorMessage);

            var newPlainXApiKey = _apiKeysService.GenerateXApiKey();
            var currentDateTime = DateTime.UtcNow;

            var model = new UserXApiKeyEntityModel()
            {
                UserName = userName,
                XApiKey = newPlainXApiKey.ToSha512HashedBase64EncodedString(),
                ValidFrom = currentDateTime,
                ValidTo = currentDateTime.AddHours(1)
            };

            ResultModel<int> resultModel = null;
            int counter = 0;

            do
            {
                resultModel = await _usersXApiKeysRepository.UpsertXApiKey(model);
                counter++;
            } while (resultModel?.Result != 1 && counter < 10 && resultModel?.Exception?.InnerException?.Message != null && resultModel.Exception.InnerException.Message.Contains("Duplicate entry"));

            if (resultModel?.Result != 1)
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Unable to generate 'X-API-Key'.");

            model.XApiKey = newPlainXApiKey;
            return Ok(model.ToUserXApiKeyApiModel());
        }
    }
}
