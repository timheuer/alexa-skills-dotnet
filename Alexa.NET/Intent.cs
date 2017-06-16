using Newtonsoft.Json;
using System.Collections.Generic;

namespace Alexa.NET
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