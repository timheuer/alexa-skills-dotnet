using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Alexa.NET.ListManagement
{
    internal class LongDateConverter:JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString("ddd MMM dd HH:mm:ss zzz yyyy").Replace("+00:00", "UTC"));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTime.ParseExact(reader.ReadAsString().Replace("UTC", "+00:00"), "ddd MMM dd HH:mm:ss zzz yyyy",
                CultureInfo.InvariantCulture);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
    }
}