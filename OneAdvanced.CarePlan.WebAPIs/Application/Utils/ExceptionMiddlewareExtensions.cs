using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OneAdvanced.CarePlan.WebAPIs.Application.Models;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Utils
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new InternalServerErrorDescription()
                    {
                        Message = "Internal Server Error."
                    }));
                });
            });
        }
    }
}