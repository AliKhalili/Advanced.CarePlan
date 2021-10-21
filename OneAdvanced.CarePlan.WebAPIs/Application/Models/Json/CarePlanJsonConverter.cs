using System;
using Newtonsoft.Json;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Models.Json
{
    //public class CarePlanJsonConverter : JsonConverter<IGetCarePlanModel>
    //{
    //    public override void WriteJson(JsonWriter writer, IGetCarePlanModel value, JsonSerializer serializer)
    //    {
    //        if (value.IsCompleted().Equals("no", StringComparison.OrdinalIgnoreCase))
    //            serializer.Serialize(writer, (GetCarePlanModel)value);
    //        else
    //            serializer.Serialize(writer, (GetUncompletedCarePlanModel)value);
    //    }

    //    public override IGetCarePlanModel ReadJson(JsonReader reader, Type objectType, IGetCarePlanModel existingValue,
    //        bool hasExistingValue, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}