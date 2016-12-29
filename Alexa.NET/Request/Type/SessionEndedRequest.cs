using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class SessionEndedRequest : Request
    {
        [JsonProperty("reason")]
        string Reason { get; set; }
    }
}