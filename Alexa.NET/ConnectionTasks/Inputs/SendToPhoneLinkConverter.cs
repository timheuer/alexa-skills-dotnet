using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class SendToPhoneLinkConverter : JsonConverter<ISendToPhoneLink>
    {
        public override void WriteJson(JsonWriter writer, ISendToPhoneLink value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var propertyName = PropertyNameFromType(value);

            writer.WriteStartObject();
            writer.WritePropertyName(propertyName);
            serializer.Serialize(writer, value, value.GetType());
            writer.WriteEndObject();
        }

        public override ISendToPhoneLink ReadJson(JsonReader reader, Type objectType, ISendToPhoneLink existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            //move past start object
            reader.Read();
            var linkObject = TypeFromPropertyName(reader.Value.ToString());

            if (linkObject == null)
            {
                throw new InvalidOperationException("Unable to parse link type " + reader.Value);
            }

            //move to link object
            reader.Read();

            if (reader.TokenType == JsonToken.StartObject)
            {
                serializer.Populate(reader, linkObject);
            }

            //and end the object;
            reader.Read();

            return linkObject;
        }

        private string PropertyNameFromType(ISendToPhoneLink value)
        {
            switch (value)
            {
                default:
                    return "UNIVERSAL_LINK";
            }
        }

        private ISendToPhoneLink TypeFromPropertyName(string name)
        {
            return name switch
            {
                "UNIVERSAL_LINK" => new STPUniversalLink(),
                _ => null
            };
        }
    }
}