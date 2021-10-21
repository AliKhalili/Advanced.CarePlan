using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Utils.Swagger
{
    public class CustomSwaggerDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Components.Schemas.Remove("ProblemDetails");
        }
    }
}