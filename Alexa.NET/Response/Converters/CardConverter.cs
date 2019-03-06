using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alexa.NET.Response.Converters
{
    public class CardConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanRead => true;

        public static Dictionary<string, Func<ICard>> TypeFactories = new Dictionary<string, Func<ICard>>
        {
            { "Simple", () => new SimpleCard() },
            { "Standard", () => new StandardCard() },
            { "LinkAccount", () => new LinkAccountCard() },
            { "AskForPermissionsConsent", () => new AskForPermissionsConsentCard() }
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
                    $"unrecognized card type '{typeValue}'"
                );

            var card = TypeFactories[typeValue]();

            serializer.Populate(jsonObject.CreateReader(), card);

            return card;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ICard);
        }
    }
}