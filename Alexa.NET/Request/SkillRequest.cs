using Alexa.NET.Request.Type;
using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class SkillRequest
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("session")]
        public Session Session { get; set; }

        [JsonProperty("context")]
        public Context Context { get; set; }

        [JsonProperty("request")]
        [JsonConverter(typeof(RequestConverter))]
        public Type.Request Request { get; set; }

        public System.Type GetRequestType()
        {
            return Request?.GetType();
        }
    }
}