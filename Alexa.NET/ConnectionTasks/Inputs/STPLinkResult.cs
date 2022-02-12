using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPLinkResult
    {
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public STPResultStatus Status { get; set; }

        [JsonProperty("errorCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorCode { get; set; }
    }
}