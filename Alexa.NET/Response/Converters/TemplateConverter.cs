using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response.Directive;
using Alexa.NET.Response.Directive.Templates.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Response.Converters
{
    public class TemplateConverter : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => false;

        public static Dictionary<string, Func<ITemplate>> TypeFactories = new Dictionary<string, Func<ITemplate>>
        {
            { "BodyTemplate1", () => new BodyTemplate1() },
            { "BodyTemplate2", () => new BodyTemplate2() },
            { "BodyTemplate3", () => new BodyTemplate3() },
            { "BodyTemplate6", () => new BodyTemplate6() },
            { "BodyTemplate7", () => new BodyTemplate7() },
            { "ListTemplate1", () => new ListTemplate1() },
            { "ListTemplate2", () => new ListTemplate2() },
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
                    $"unrecognized template type '{typeValue}'"
                );

            var template = TypeFactories[typeValue]();

            serializer.Populate(jsonObject.CreateReader(), template);

            return template;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}