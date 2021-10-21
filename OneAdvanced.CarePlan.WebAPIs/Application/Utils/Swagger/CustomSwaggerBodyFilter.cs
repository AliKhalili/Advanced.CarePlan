using System.Collections.Concurrent;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Utils.Swagger
{
    public class CustomSwaggerBodyFilter : IRequestBodyFilter
    {
        public void Apply(OpenApiRequestBody requestBody, RequestBodyFilterContext context)
        {
            requestBody.Content = new ConcurrentDictionary<string, OpenApiMediaType>()
            {
                ["application/json"] = requestBody.Content["application/json"]
            };
        }
    }
}