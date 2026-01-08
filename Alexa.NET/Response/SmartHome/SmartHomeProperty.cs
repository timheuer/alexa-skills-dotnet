using Newtonsoft.Json;
using System;

namespace Alexa.NET.Response.SmartHome
{
    public class SmartHomeProperty
    {
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }

        [JsonProperty("timeOfSample")]
        public DateTime TimeOfSample { get; set; }

        [JsonProperty("uncertaintyInMilliseconds")]
        public int UncertaintyInMilliseconds { get; set; }
    }
}
