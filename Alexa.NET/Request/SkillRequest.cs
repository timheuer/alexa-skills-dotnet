using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class SkillRequest
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("session")]
        public Session Session { get; set; }

        [JsonProperty("request")]
        public RequestBundle Request { get; set; }

        public System.Type GetRequestType()
        {
            return Request?.GetRequestType();
        }
    }
}