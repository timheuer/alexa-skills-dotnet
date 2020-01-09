using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Alexa.NET.Response
{
    public class CanFulfillIntentResponse
    {
        [JsonProperty("canFulfill")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CanFulfill CanFulfill { get; set; }

        [JsonProperty("slots")]
        public Dictionary<string, CanFulfillSlot> Slots { get; set; } = new Dictionary<string, CanFulfillSlot>();


    }


    public class CanFulfillSlot
    {
        [JsonProperty("canFulfill")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CanFulfill CanFulfill { get; set; }

        [JsonProperty("canUnderstand")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CanFulfill CanUnderstand { get; set; }
    }

    public enum CanFulfill
    {
        YES,
        NO,
        MAYBE
    }
}