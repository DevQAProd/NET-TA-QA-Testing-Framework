using System.Net;
using ApplicationName.Dev.Api.Interfaces;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.HelperModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApplicationName.Dev.Api.Attributes
{
    public class BasicAuthenticationWithHeaderAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out var headerValue);
            var authenticationService = context.HttpContext.RequestServices.GetService(typeof(IAuthenticationService)) as IAuthenticationService;
            AuthValidationResultModel result = authenticationService?.IsBasicAuthenticationValid(headerValue)?.Result;

            if (result == null || !result.IsAuthValid)
                context.Result = new ObjectResult(result?.ErrorMessage + " Please include valid UserName and Password in 'Authorization' Header of the HTTP Request using Basic Authentication Scheme.") { StatusCode = (int)HttpStatusCode.Unauthorized };
        }
    }
}
