using Newtonsoft.Json;
using System;

namespace Alexa.NET.Response.SmartHome
{
    /// <summary>
    /// Represents a single device property state for reporting to Alexa
    /// </summary>
    public class SmartHomeProperty
    {
        /// <summary>
        /// The namespace of the property (e.g., Alexa.PowerController)
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// The name of the property (e.g., powerState, brightness)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The current value of the property (e.g., "ON", "OFF", 75)
        /// </summary>
        [JsonProperty("value")]
        public object Value { get; set; }

        /// <summary>
        /// The timestamp when this property value was sampled
        /// </summary>
        [JsonProperty("timeOfSample")]
        public DateTime TimeOfSample { get; set; }

        /// <summary>
        /// The uncertainty of the property value in milliseconds
        /// </summary>
        [JsonProperty("uncertaintyInMilliseconds")]
        public int UncertaintyInMilliseconds { get; set; }
    }
}
