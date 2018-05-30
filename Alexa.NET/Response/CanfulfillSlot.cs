using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Response
{
    public class CanfulfillSlot
    {
        [JsonProperty("canUnderstand"),JsonConverter(typeof(StringEnumConverter))]
        public CanUnderstand CanUnderstand { get; set; }

        [JsonProperty("canFulfill"),JsonConverter(typeof(StringEnumConverter))]
        public SlotCanFulfill CanFulfill { get; set; }
    }
}