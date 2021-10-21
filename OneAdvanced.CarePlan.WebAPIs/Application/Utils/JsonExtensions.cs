using System;
using System.Reflection;
using Newtonsoft.Json;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Utils
{
    public static class JsonExtensions
    {
        public static string GetJsonProperty<T>(this T obj, string name)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var property = obj.GetType().GetProperty(name)?.GetCustomAttribute<JsonPropertyAttribute>();
            if (property == null)
                throw new ArgumentException($"Property {name} was not found.");
            return property.PropertyName;
        }
    }
}