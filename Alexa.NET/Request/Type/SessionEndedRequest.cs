using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Request.Type
{
    public class SessionEndedRequest : Request
    {
        [JsonProperty("reason")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Reason Reason { get; set; }
    }
}