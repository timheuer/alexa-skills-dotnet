using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class PinConfirmationResult
    {
        [JsonProperty("status",NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public PinConfirmationStatus Status { get; set; }

        [JsonProperty("reason",NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public PinConfirmationReason Reason { get; set; }
    }
}
