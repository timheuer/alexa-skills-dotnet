using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPDirectLaunchResult
    {
        [JsonProperty("primary",NullValueHandling = NullValueHandling.Ignore)]
        public STPLinkResult Primary { get; set; }

        [JsonProperty("fallback",NullValueHandling = NullValueHandling.Ignore)]
        public STPLinkResult Fallback { get; set; }
    }
}