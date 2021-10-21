using System;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Utils.Swagger
{
    public class CustomSwaggerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var property = context.MemberInfo;
            if(property == null)
                return;

            var jsonNameAttr = property.GetCustomAttribute<JsonPropertyAttribute>();
            var propertySchemaAttribute = property.GetCustomAttribute<OpenApiPropertySchemaAttribute>();
            
            if (propertySchemaAttribute != null)
            {
                var name = jsonNameAttr?.PropertyName ?? property.Name;
                schema.Title = name;
                
                if (!string.IsNullOrEmpty(propertySchemaAttribute.Description))
                    schema.Description = propertySchemaAttribute.Description;

                schema.Nullable = property.IsNonNullableReferenceType();
                schema.ReadOnly = propertySchemaAttribute.ReadOnly;

                if (!string.IsNullOrEmpty(propertySchemaAttribute.Format))
                    schema.Format = propertySchemaAttribute.Format;

                if (!string.IsNullOrEmpty(propertySchemaAttribute.Example))
                    schema.Example = new OpenApiString(propertySchemaAttribute.Example);
                return;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class OpenApiClassSchemaAttribute : Attribute
    {
        /// <summary>
        /// A brief description of the object. This could contain examples of use.
        /// </summary>
        public string Description { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class OpenApiPropertySchemaAttribute : Attribute
    {
        /// <summary>
        /// Follow JSON Schema definition. Short text providing information about the data.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A brief description of the parameter. This could contain examples of use.
        /// CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Determines whether this parameter is mandatory.
        /// If the parameter location is "path", this property is REQUIRED and its value MUST be true.
        /// Otherwise, the property MAY be included and its default value is false.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// While relying on JSON Schema's defined formats,
        /// the OAS offers a few additional predefined formats.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Relevant only for Schema "properties" definitions. Declares the property as "read only".
        /// This means that it MAY be sent as part of a response but SHOULD NOT be sent as part of the request.
        /// If the property is marked as readOnly being true and is in the required list,
        /// the required will take effect on the response only.
        /// A property MUST NOT be marked as both readOnly and writeOnly being true.
        /// Default value is false.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// A free-form property to include an example of an instance for this schema.
        /// To represent examples that cannot be naturally represented in JSON or YAML,
        /// a string value can be used to contain the example with escaping where necessary.
        /// </summary>
        public string Example { get; set; }

        /// <summary>
        /// Allows sending a null value for the defined schema. Default value is false.
        /// </summary>
        public bool Nullable { get; set; }

    }
}