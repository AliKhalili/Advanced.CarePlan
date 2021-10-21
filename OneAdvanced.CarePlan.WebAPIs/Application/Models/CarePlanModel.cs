using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using OneAdvanced.CarePlan.WebAPIs.Application.Infrastructures;
using OneAdvanced.CarePlan.WebAPIs.Application.Utils;
using OneAdvanced.CarePlan.WebAPIs.Application.Utils.Hypermedia;
using OneAdvanced.CarePlan.WebAPIs.Application.Utils.Swagger;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Models
{
    [OpenApiClassSchema]
    public class CarePlanModel
    {
        [JsonProperty("care_plan_id")]
        [OpenApiPropertySchema(
            Title = "Care Plan Id",
            Description = "An auto-increment number filled by application in create.",
            ReadOnly = true
        )]
        public virtual int CarePlanId { get; set; }

        [JsonProperty("title")]
        [OpenApiPropertySchema(
            Title = "Title",
            Description = "Title of the care plan.",
            Required = true,
            Example = "Feeding care plan"
        )]
        public virtual string Title { get; set; }

        [JsonProperty("patient_name")]
        [OpenApiPropertySchema(
            Title = "Patient Name",
            Description = "Patient name of the care plan.",
            Required = true,
            Example = "Dorris Day"
        )]
        public virtual string PatientName { get; set; }

        [JsonProperty("create_by_user")]
        [OpenApiPropertySchema(
            Title = "Created By User",
            Description = "Name of the user that added this care plan.",
            Required = true,
            Example = "Alex.savage"
        )]
        public virtual string CreatedByUser { get; set; }

        [JsonProperty("start_date")]
        [OpenApiPropertySchema(
            Title = "Actual Start Date",
            Description = @"The date on which the care plan would start. The format is ISO 8601(YYYY-MM-DD).",
            Required = true,
            Example = "2021-07-30",
            Format = "date"
        )]
        public virtual string StartDate { get; set; }

        [JsonProperty("target_date")]
        [OpenApiPropertySchema(
            Title = "Target Date",
            Description = @"The date on which the care plan except to finish. The format is ISO 8601(YYYY-MM-DD).",
            Required = true,
            Example = "2021-08-10",
            Format = "date"
        )]
        public virtual string TargetDate { get; set; }

        [JsonProperty("reasons")]
        [OpenApiPropertySchema(
            Title = "Reason",
            Description = "Some sentences for describing the reasons for the care plan.",
            Required = true,
            Example = "Ensure client’s weight is maintained"
        )]
        public virtual string Reasons { get; set; }

        [JsonProperty("actions")]
        [OpenApiPropertySchema(
            Title = "Action",
            Description = "Some sentences for describing the actions of the care plan.",
            Required = true,
            Example = "3 hot meals each day and encourage the client to eat at least 75% of each meal. Provide 1 glass of water with each meal."
        )]
        public virtual string Actions { get; set; }

        [JsonProperty("frequency")]
        [OpenApiPropertySchema(
            Title = "Frequency",
            Description = "Some sentences for describing the frequency of the care plan.",
            ReadOnly = false,
            Example = "3 times a day"
        )]
        public virtual string Frequency { get; set; }

        [JsonProperty("completed")]
        [OpenApiPropertySchema(
            Title = "Completed",
            Description = "Indicate is care plan completed or not.",
            ReadOnly = false,
            Example = "Yes or No"
        )]
        public virtual string Completed { get; set; }

        [JsonProperty("end_date")]
        [OpenApiPropertySchema(
            Title = "End Date",
            Description = @"The date on which the care plan actually finish. The format is ISO 8601(YYYY-MM-DD).",
            Required = true,
            Example = "2021-08-20",
            Format = "date"
        )]
        public virtual string EndDate { get; set; }

        [JsonProperty("outcomes")]
        [OpenApiPropertySchema(
            Title = "Outcome",
            Description = "Some sentences for describing the outcomes of the care plan.",
            ReadOnly = false,
            Example = "Increase quality of life."
        )]
        public virtual string Outcomes { get; set; }


        [JsonProperty("_links", Order = 999)]
        public virtual HyperLink[] Links { get; set; }
        public void Apply(params HyperLink[] links)
        {
            Links = links;
        }

        private DateTime? _startDate;
        private DateTime? _targetDate;
        private DateTime? _endDate;
        private bool _isCompleted;

        public DateTime GetStartDate() => _startDate ?? throw new InvalidOperationException(nameof(GetStartDate));
        public DateTime GetTargetDate() => _targetDate ?? throw new InvalidOperationException(nameof(GetTargetDate));
        public DateTime? GetEndDateDate() => _endDate;
        public bool GetIsCompleted() => _isCompleted;
        public (bool isValid, ErrorsDescription errorsDescription) Validate()
        {
            var errors = new List<ErrorDescriptionItem>();

            if (string.IsNullOrWhiteSpace(Title))
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(Title)), ErrorType.MISSING_MANDATORY_PROPERTY, $"The {nameof(Title)} is mandatory."));
            else if (Title.Length > 450)
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(Title)), ErrorType.MAXIMUM_LENGTH_EXCEEDED, $"The {nameof(Title)} value cannot exceed {450} characters."));


            if (string.IsNullOrWhiteSpace(PatientName))
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(PatientName)), ErrorType.MISSING_MANDATORY_PROPERTY, $"The {nameof(PatientName)} is mandatory."));
            else if (PatientName.Length > 450)
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(PatientName)), ErrorType.MAXIMUM_LENGTH_EXCEEDED, $"The {nameof(PatientName)} value cannot exceed {450} characters."));

            if (string.IsNullOrWhiteSpace(CreatedByUser))
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(CreatedByUser)), ErrorType.MISSING_MANDATORY_PROPERTY, $"The {nameof(CreatedByUser)} is mandatory."));
            else if (CreatedByUser.Length > 450)
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(CreatedByUser)), ErrorType.MAXIMUM_LENGTH_EXCEEDED, $"The {nameof(CreatedByUser)} value cannot exceed {450} characters."));

            if (string.IsNullOrWhiteSpace(StartDate))
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(StartDate)), ErrorType.MISSING_MANDATORY_PROPERTY, $"The {nameof(StartDate)} is mandatory."));
            else if (!DateTime.TryParseExact(StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate))
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(StartDate)), ErrorType.INVALID_DATE_FORMAT, $"The {nameof(StartDate)} format is not correct. expected is  ISO 8601(YYYY-MM-DD), E.g. 2021-07-30."));
            else
                _startDate = startDate;

            if (string.IsNullOrWhiteSpace(TargetDate))
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(TargetDate)), ErrorType.MISSING_MANDATORY_PROPERTY, $"The {nameof(TargetDate)} is mandatory."));
            else if (!DateTime.TryParseExact(TargetDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var targetDate))
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(TargetDate)), ErrorType.INVALID_DATE_FORMAT, $"The {nameof(TargetDate)} format is not correct. expected is  ISO 8601(YYYY-MM-DD), E.g. 2021-07-30."));
            else
                _targetDate = targetDate;

            if (string.IsNullOrWhiteSpace(Reasons))
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(Reasons)), ErrorType.MISSING_MANDATORY_PROPERTY, $"The {nameof(Reasons)} is mandatory."));
            else if (Reasons.Length > 1000)
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(Reasons)), ErrorType.MAXIMUM_LENGTH_EXCEEDED, $"The {nameof(Reasons)} value cannot exceed {1000} characters."));

            if (string.IsNullOrWhiteSpace(Actions))
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(Actions)), ErrorType.MISSING_MANDATORY_PROPERTY, $"The {nameof(Actions)} is mandatory."));
            else if (Actions.Length > 1000)
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(Actions)), ErrorType.MAXIMUM_LENGTH_EXCEEDED, $"The {nameof(Actions)} value cannot exceed {1000} characters."));

            if (!string.IsNullOrWhiteSpace(Frequency) && Frequency.Length > 1000)
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(Frequency)), ErrorType.MAXIMUM_LENGTH_EXCEEDED, $"The {nameof(Frequency)} value cannot exceed {1000} characters."));

            if (!string.IsNullOrEmpty(Completed) && Completed.Equals("yes", StringComparison.InvariantCultureIgnoreCase))
            {
                _isCompleted = true;
                if (string.IsNullOrWhiteSpace(EndDate))
                    errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(EndDate)), ErrorType.MISSING_MANDATORY_PROPERTY, $"The {nameof(EndDate)} is mandatory because care plan is completed."));
                else if (!DateTime.TryParseExact(EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDate))
                    errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(EndDate)), ErrorType.INVALID_DATE_FORMAT, $"The {nameof(EndDate)} format is not correct. expected is  ISO 8601(YYYY-MM-DD), E.g. 2021-07-30."));
                else if (_startDate.HasValue && _startDate >= endDate)
                    errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(EndDate)), ErrorType.END_DATE_IS_LOWER_THAN_START_DATE, $"The {nameof(EndDate)} value must be greater than {nameof(StartDate)} value."));
                else
                    _endDate = endDate;

                if (string.IsNullOrWhiteSpace(Outcomes))
                    errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(Outcomes)), ErrorType.MISSING_MANDATORY_PROPERTY, $"The {nameof(Outcomes)} is mandatory because care plan is completed."));
                else if (Outcomes.Length > 1000)
                    errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(Outcomes)), ErrorType.MAXIMUM_LENGTH_EXCEEDED, $"The {nameof(Outcomes)} value cannot exceed {1000} characters."));
            }
            else if (!string.IsNullOrEmpty(Completed) && !Completed.Equals("no", StringComparison.InvariantCultureIgnoreCase))
                errors.Add(new ErrorDescriptionItem(this.GetJsonProperty(nameof(Completed)), ErrorType.INVALID_PICK_LIST_FORMAT, $"The {nameof(Completed)} value is not correct. expected is Yes or No, E.g. Yes."));

            if (errors.Any())
                return (false, ErrorsDescription.New(errors));

            return (true, ErrorsDescription.EmptyErrorsDescription());
        }

        public CarePlanEntity ToEntity()
        {
            return new CarePlanEntity()
            {
                Title = Title,
                PatientName = PatientName,
                CreatedByUser = CreatedByUser,
                StartDate = GetStartDate(),
                TargetDate = GetTargetDate(),
                Reasons = Reasons,
                Actions = Actions,
                Frequency = Frequency,
                Completed = GetIsCompleted(),
                EndDate = GetEndDateDate(),
                Outcomes = Outcomes
            };
        }
    }
}