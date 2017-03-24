using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Alexa.NET.Request.Type
{
    public class RequestConverter : JsonConverter
    {
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
            //AudioPlayer and PlaybackController requests are very similar,
            //  map them to respective types
            if (requestType.StartsWith("AudioPlayer"))
                requestType = "AudioPlayer";
            else if (requestType.StartsWith("PlaybackController"))
                requestType = "PlaybackController";

            switch (requestType)
            {
                case "IntentRequest":
                    return new IntentRequest();
                case "LaunchRequest":
                    return new LaunchRequest();
                case "SessionEndedRequest":
                    return new SessionEndedRequest();
                case "AudioPlayer":
                    return new AudioPlayerRequest();
                case "PlaybackController":
                    return new PlaybackControllerRequest();
                case "System.ExceptionEncountered":
                    return new SystemExceptionRequest();
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), $"Unknown request type: {requestType}.");
            }
        }
    }
}