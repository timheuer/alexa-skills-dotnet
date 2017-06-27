using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Intent
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("confirmationStatus")]
        public string ConfirmationStatus { get; set; }

        [JsonProperty("slots")]
        public Dictionary<string, Slot> Slots { get; set; }
    }
}