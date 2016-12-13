using Newtonsoft.Json;
using System.Collections.Generic;

namespace Alexa.NET.Response
{
    public class SkillResponse
    {
        [JsonRequired]
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("sessionAttributes", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> SessionAttributes { get; set; }

        [JsonRequired]
        [JsonProperty("response")]
        public ResponseBody Response { get; set; }
    }
}