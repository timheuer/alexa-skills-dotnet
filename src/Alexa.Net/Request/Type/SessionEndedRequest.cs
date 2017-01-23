using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class SessionEndedRequest : Request
    {
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}