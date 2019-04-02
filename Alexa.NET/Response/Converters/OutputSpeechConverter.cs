using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Response.Converters
{
    public class OutputSpeechConverter : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => false;

        public static Dictionary<string, Func<IOutputSpeech>> TypeFactories = new Dictionary<string, Func<IOutputSpeech>>
        {
            { "SSML", () => new SsmlOutputSpeech() },
            { "PlainText", () => new PlainTextOutputSpeech() },
        };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var typeKey = jsonObject["type"] ?? jsonObject["Type"];
            var typeValue = typeKey.Value<string>();
            var hasFactory = TypeFactories.ContainsKey(typeValue);

            if (!hasFactory)
                throw new Exception(
                    $"unable to deserialize response. " +
                    $"unrecognized output speech type '{typeValue}'"
                );

            var speech = TypeFactories[typeValue]();

            serializer.Populate(jsonObject.CreateReader(), speech);

            return speech;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IOutputSpeech);
        }
    }
}