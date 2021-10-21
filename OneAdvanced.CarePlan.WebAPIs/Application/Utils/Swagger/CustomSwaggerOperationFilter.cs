using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Utils.Swagger
{
    public class CustomSwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Responses.ContainsKey("404"))
            {
                operation.Responses["404"] = new OpenApiResponse()
                {
                    Description = operation.Responses["404"].Description
                };
            }
        }
    }
}