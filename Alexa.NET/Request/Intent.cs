using Newtonsoft.Json;
using System.Collections.Generic;

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
        public IntentSignature Signature { get; set; }


        [JsonProperty("slots")]
        public Dictionary<string, Slot> Slots { get; set; }
    }
}