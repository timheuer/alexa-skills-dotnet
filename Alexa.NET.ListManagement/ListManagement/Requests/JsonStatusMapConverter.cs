using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Alexa.NET.ListManagement.Requests
{
    internal class JsonStatusMapConverter:JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var maps = serializer.Deserialize<StatusMap[]>(reader);

            return maps.ToDictionary(sm => sm.Status, sm => sm.Href);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IDictionary<string, Uri>);
        }
    }
}