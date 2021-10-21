using Newtonsoft.Json;
using OneAdvanced.CarePlan.WebAPIs.Application.Utils.Hypermedia;
using OneAdvanced.CarePlan.WebAPIs.Application.Utils.Swagger;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Models
{
    [OpenApiClassSchema(
        Description = "An model that represents a new care plan."
    )]
    public class CreateCarePlanModel : CarePlanModel
    {
        [JsonIgnore]
        public override int CarePlanId { get; set; }

        [JsonIgnore]
        public override HyperLink[] Links { get; set; }
    }
}