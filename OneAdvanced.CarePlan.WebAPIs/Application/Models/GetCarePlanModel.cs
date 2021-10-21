using Newtonsoft.Json;
using OneAdvanced.CarePlan.WebAPIs.Application.Infrastructures;
using System;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Models
{
    public class GetCarePlanModel : CarePlanModel
    {
        public static explicit operator GetCarePlanModel(CarePlanEntity entity)
        {
            return new GetCarePlanModel
            {
                CarePlanId = entity.CarePlanId,
                Title = entity.Title,
                PatientName = entity.PatientName,
                CreatedByUser = entity.CreatedByUser,
                StartDate = entity.StartDate.ToString("yyyy-MM-dd"),
                TargetDate = entity.TargetDate.ToString("yyyy-MM-dd"),
                Reasons = entity.Reasons,
                Actions = entity.Actions,
                Frequency = entity.Frequency,
                Completed = entity.Completed ? "Yes" : "No",
                EndDate = entity.EndDate.GetValueOrDefault().ToString("yyyy-MM-dd"),
                Outcomes = entity.Outcomes
            };
        }
    }

    public class GetUncompletedCarePlanModel : GetCarePlanModel
    {
        [JsonIgnore]
        public override string EndDate { get; set; }
        [JsonIgnore]
        public override string Outcomes { get; set; }

        public static explicit operator GetUncompletedCarePlanModel(CarePlanEntity entity)
        {
            return new GetUncompletedCarePlanModel
            {
                CarePlanId = entity.CarePlanId,
                Title = entity.Title,
                PatientName = entity.PatientName,
                CreatedByUser = entity.CreatedByUser,
                StartDate = entity.StartDate.ToString("yyyy-MM-dd"),
                TargetDate = entity.TargetDate.ToString("yyyy-MM-dd"),
                Reasons = entity.Reasons,
                Actions = entity.Actions,
                Frequency = entity.Frequency,
                Completed = entity.Completed ? "Yes" : "No",
                EndDate = entity.EndDate.GetValueOrDefault().ToString("yyyy-MM-dd"),
                Outcomes = entity.Outcomes
            };
        }
    }
}