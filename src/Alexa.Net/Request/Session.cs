using Newtonsoft.Json;
using System.Collections.Generic;

namespace Alexa.NET.Request
{
    public class Session
    {
        [JsonProperty("new")]
        public bool New { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("attributes")]
        public Dictionary<string, object> Attributes { get; set; }

        [JsonProperty("application")]
        public Application Application { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}