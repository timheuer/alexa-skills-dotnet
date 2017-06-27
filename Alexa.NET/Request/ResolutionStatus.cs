using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class ResolutionStatus
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
