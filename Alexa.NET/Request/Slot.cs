using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Slot
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("confirmationStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfirmationStatus { get; set; }

        [JsonProperty("source",NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }

        [JsonProperty("resolutions", NullValueHandling = NullValueHandling.Ignore)]
        public Resolution Resolution { get; set; }

        [JsonProperty("slotValue",NullValueHandling = NullValueHandling.Ignore)]
        public SlotValue SlotValue { get; set; }
    }
}