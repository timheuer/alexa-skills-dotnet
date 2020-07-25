using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Request
{
    public class SlotValue
    {
        [JsonProperty("type",NullValueHandling = NullValueHandling.Ignore),
         JsonConverter(typeof(StringEnumConverter))]
        public SlotValueType SlotType { get; set; }

        [JsonProperty("value",NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("values",NullValueHandling = NullValueHandling.Ignore)]
        public SlotValue[] Values { get; set; }

        [JsonProperty("resolutions",NullValueHandling = NullValueHandling.Ignore)]
        public Resolution Resolutions { get; set; }
    }
}