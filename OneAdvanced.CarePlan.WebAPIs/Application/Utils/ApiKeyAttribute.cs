using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneAdvanced.CarePlan.WebAPIs.Application.Models;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Utils
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyName = "X-API-Key";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyName, out var extractedApiKey))
            {
                context.Result = new JsonResult(new ErrorsDescription
                {
                    Message = "Unauthorized access.",
                    Errors = new[] { new ErrorDescriptionItem(ApiKeyName, ErrorType.API_KEY_MISSING, "Api Key was not provided.") }
                });
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(ApiKeyName);

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new JsonResult(new ErrorsDescription
                {
                    Message = "Unauthorized access.",
                    Errors = new[] { new ErrorDescriptionItem(ApiKeyName, ErrorType.API_KEY_INVALID, "Api Key is not valid.") }
                });
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await next();
        }
    }
}