using System;
using System.Collections.Generic;
using Alexa.NET.Response.Directive.ConnectionTasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Response.Converters
{
    public class ConnectionTaskConverter:JsonConverter
    {
        public static Dictionary<string, Func<IConnectionTask>> TaskFactoryFromUri = new Dictionary<string, Func<IConnectionTask>>
        {
            {"connection://AMAZON.PrintPDF/1",() => new PrintPdfV1() }
        };

        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var typeKey = jsonObject["uri"] ?? jsonObject["Uri"];
            var typeValue = typeKey.Value<string>();
            var hasFactory = TaskFactoryFromUri.ContainsKey(typeValue);

            if (!hasFactory)
                throw new Exception(
                    $"unable to deserialize response. " +
                    $"unrecognized directive type '{typeValue}'"
                );

            var directive = TaskFactoryFromUri[typeValue]();

            serializer.Populate(jsonObject.CreateReader(), directive);

            return directive;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IConnectionTask);
        }
    }
}