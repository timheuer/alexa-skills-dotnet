using Newtonsoft.Json;

namespace Alexa.NET
{
    public class ResolutionStatus
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
