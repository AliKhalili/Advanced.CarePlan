
using Newtonsoft.Json;
using OneAdvanced.CarePlan.WebAPIs.Application.Utils.Swagger;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Utils.Hypermedia
{
    public class HyperLink
    {
        [JsonProperty("href")]
        [OpenApiPropertySchema(
            Title = "href",
            Description = "Address of the API.",
            ReadOnly = true,
            Example = "/api/care-plans/3"
        )]
        public string Href { get; set; }

        [JsonProperty("rel")]
        [OpenApiPropertySchema(
            Title = "rel",
            Description = "Type of the API.",
            ReadOnly = true,
            Example = "delete_care"
        )]
        public string Rel { get; set; }

        [JsonProperty("method")]
        [OpenApiPropertySchema(
            Title = "method",
            Description = "HTTP method of the API.",
            ReadOnly = true,
            Example = "DELETE"
        )]
        public string Method { get; set; }

        public HyperLink(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}