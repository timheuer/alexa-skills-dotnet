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
                case STPAndroidCommonIntentLink _:
                    return "ANDROID_COMMON_INTENT";
                case STPAndroidPackageLink _:
                    return "ANDROID_PACKAGE";
                case STPCommonSchemeLink _:
                    return "COMMON_SCHEME";
                case STPCustomSchemeLink _:
                    return "CUSTOM_SCHEME";
                case STPAndroidCustomIntentLink _:
                    return "ANDROID_CUSTOM_INTENT";
                case STPWebsiteLink _:
                    return "WEBSITE_LINK";
                default:
                    return "UNIVERSAL_LINK";
            }
        }

        private ISendToPhoneLink TypeFromPropertyName(string name)
        {
            return name switch
            {
                "ANDROID_COMMON_INTENT" => new STPAndroidCommonIntentLink(),
                "ANDROID_PACKAGE" => new STPAndroidPackageLink(),
                "COMMON_SCHEME" => new STPCommonSchemeLink(),
                "CUSTOM_SCHEME" => new STPCustomSchemeLink(),
                "ANDROID_CUSTOM_INTENT" => new STPAndroidCustomIntentLink(),
                "WEBSITE_LINK" => new STPWebsiteLink(),
                "UNIVERSAL_LINK" => new STPUniversalLink(),
                _ => null
            };
        }
    }
}