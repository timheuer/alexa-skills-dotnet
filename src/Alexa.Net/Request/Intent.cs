using Newtonsoft.Json;
using System.Collections.Generic;

namespace Alexa.NET.Request
{
    public class Intent
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slots")]
        public Dictionary<string, Slot> Slots { get; set; }
    }
}