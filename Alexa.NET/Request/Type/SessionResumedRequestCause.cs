using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class SessionResumedRequestCause
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("status")]
        public SessionResumedRequestCauseStatus Status { get; set; }

        [JsonProperty("result")]
        public object Result { get; set; }
    }
}