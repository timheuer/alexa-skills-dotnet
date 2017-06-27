using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Intent
    {
        private string _name;

        [JsonProperty("name")]
        public string Name {
            get { return _name; }
            set {
                _name = value;
                Signature = value;
            }
        }

        [JsonIgnore]
        public IntentSignature Signature { get; private set; }


        [JsonProperty("confirmationStatus")]
        public string ConfirmationStatus { get; set; }

        [JsonProperty("slots")]
        public Dictionary<string, Slot> Slots { get; set; }
    }
}