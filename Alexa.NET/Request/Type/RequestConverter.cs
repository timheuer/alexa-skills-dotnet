using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Alexa.NET.Request.Type
{
    public class RequestConverter : JsonConverter
    {
        public static readonly List<IRequestTypeConverter> RequestConverters = new List<IRequestTypeConverter>(new IRequestTypeConverter[]
        {
            new DefaultRequestTypeConverter(),
            new AudioPlayerRequestTypeConverter(),
            new PlaybackRequestTypeConverter(),
            new TemplateEventRequestTypeConverter(),
            new SkillEventRequestTypeConverter()
        });

        public override bool CanWrite => false;

        public override bool CanConvert(System.Type objectType)
        {
            return objectType == typeof(Request);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            var jObject = JObject.Load(reader);

            // Create target request object based on "type" property
            var target = Create(jObject["type"].Value<string>());

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public Request Create(string requestType)
        {
            var converter = RequestConverters.FirstOrDefault(c => c.CanConvert(requestType));
            return converter?.Convert(requestType) ?? throw new ArgumentOutOfRangeException(nameof(Type), $"Unknown request type: {requestType}.");
        }
    }
}