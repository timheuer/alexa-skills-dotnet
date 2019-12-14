using Alexa.NET.ConnectionTasks;
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
        public ConnectionStatus Status { get; set; }

        [JsonProperty("result")]
        public object Result { get; set; }
    }
}