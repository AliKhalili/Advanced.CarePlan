using Newtonsoft.Json;
using OneAdvanced.CarePlan.WebAPIs.Application.Utils.Hypermedia;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Models
{
    public class UpdateCarePlanModel : CarePlanModel
    {
        [JsonIgnore]
        public override int CarePlanId { get; set; }

        [JsonIgnore]
        public override HyperLink[] Links { get; set; }
    }
}