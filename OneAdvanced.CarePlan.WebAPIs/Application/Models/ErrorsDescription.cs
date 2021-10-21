using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using OneAdvanced.CarePlan.WebAPIs.Application.Utils.Swagger;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Models
{
    public class ErrorsDescription
    {

        public static ErrorsDescription EmptyErrorsDescription()
        {
            return null;
        }

        public static ErrorsDescription New(IEnumerable<ErrorDescriptionItem> errors, string message = "Invalid request")
        {
            return new ErrorsDescription()
            {
                Message = message,
                Errors = errors.ToArray()
            };
        }

        [JsonProperty("message")]
        [OpenApiPropertySchema(
            Title = "Message",
            Description = "A general description of happened errors.",
            ReadOnly = true,
            Example = "Internal server error."
        )]
        public string Message { get; init; }

        [JsonProperty("errors")]
        [OpenApiPropertySchema(
            Title = "Errors",
            Description = "An array of happened errors with their details and type.",
            ReadOnly = true,
            Example = "{ ['source':'Title', 'type':'MISSING_MANDATORY_PROPERTY', 'message':'The Title is mandatory.'] }"
        )]
        public virtual ErrorDescriptionItem[] Errors { get; init; }
    }

    public class InternalServerErrorDescription : ErrorsDescription
    {
        public InternalServerErrorDescription()
        {
            Message = "Internal server error.";
        }

        [JsonIgnore]
        public override ErrorDescriptionItem[] Errors { get; init; }
    }

    public class ErrorDescriptionItem
    {
        public ErrorDescriptionItem(string source, ErrorType type, string message)
        {
            Source = source;
            Type = Enum.GetName(type);
            Message = message;
        }
        [JsonProperty("source")]
        [OpenApiPropertySchema(
            Title = "Source",
            Description = "The source of error.",
            ReadOnly = true,
            Example = "Patient Name"
        )]
        public string Source { get; init; }

        [JsonProperty("type")]
        [OpenApiPropertySchema(
            Title = "Message",
            Description = "The type of error.",
            ReadOnly = true,
            Example = "MAXIMUM_LENGTH_EXCEEDED"
        )]
        public string Type { get; init; }

        [JsonProperty("message")]
        [OpenApiPropertySchema(
            Title = "Message",
            Description = "The human readable message of error.",
            ReadOnly = true,
            Example = "The Patient Name value cannot exceed 450 characters."
        )]
        public string Message { get; init; }
    }

    public enum ErrorType
    {
        MISSING_MANDATORY_PROPERTY = 0,
        MAXIMUM_LENGTH_EXCEEDED = 1,
        INVALID_DATE_FORMAT = 2,
        END_DATE_IS_LOWER_THAN_START_DATE = 3,
        INVALID_PICK_LIST_FORMAT = 4,
        API_KEY_MISSING = 5,
        API_KEY_INVALID = 6
    }
}