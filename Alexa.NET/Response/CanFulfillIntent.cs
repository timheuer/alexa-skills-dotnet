using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Alexa.NET.Response
{
    public class CanFulfillIntent
    {
        [JsonProperty("canFulfill"),JsonConverter(typeof(StringEnumConverter))]
        public CanFulfill CanFulfill { get; set; }

        [JsonProperty("slots")]
        public Dictionary<string,CanfulfillSlot> Slots { get; set; } = new Dictionary<string, CanfulfillSlot>();
    }
}
