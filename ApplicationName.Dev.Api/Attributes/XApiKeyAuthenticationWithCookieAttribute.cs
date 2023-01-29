using System.Net;
using ApplicationName.Dev.Api.Interfaces;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.HelperModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApplicationName.Dev.Api.Attributes
{
    public class XApiKeyAuthenticationWithCookieAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Cookies.TryGetValue("X-API-Key", out var xApiKey);
            var authenticationService = context.HttpContext.RequestServices.GetService(typeof(IAuthenticationService)) as IAuthenticationService;
            AuthValidationResultModel result = authenticationService?.IsXApiKeyValid(xApiKey)?.Result;

            if (result == null || !result.IsAuthValid)
                context.Result = new ObjectResult(result?.ErrorMessage + " Please include valid 'X-API-Key' as Cookie in the HTTP Request.") { StatusCode = (int)HttpStatusCode.Unauthorized };
        }
    }
}
