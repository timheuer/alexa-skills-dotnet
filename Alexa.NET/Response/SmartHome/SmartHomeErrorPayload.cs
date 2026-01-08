using Newtonsoft.Json;

namespace Alexa.NET.Response.SmartHome
{
    public class SmartHomeErrorPayload
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
