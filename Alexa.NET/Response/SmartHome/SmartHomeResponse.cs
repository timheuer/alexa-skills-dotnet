using Newtonsoft.Json;

namespace Alexa.NET.Response.SmartHome
{
    /// <summary>
    /// Top-level wrapper for Smart Home responses sent to the Alexa service
    /// </summary>
    public class SmartHomeResponse
    {
        /// <summary>
        /// The event containing response header, endpoint, and payload
        /// </summary>
        [JsonProperty("event")]
        public SmartHomeEvent Event { get; set; }

        /// <summary>
        /// Optional context containing current device state properties
        /// </summary>
        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public SmartHomeContext Context { get; set; }
    }
}
