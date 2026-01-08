using Newtonsoft.Json;
using Alexa.NET.Request.SmartHome;

namespace Alexa.NET.Response.SmartHome
{
    /// <summary>
    /// Event structure for Smart Home responses containing header, endpoint, and payload
    /// </summary>
    public class SmartHomeEvent
    {
        /// <summary>
        /// Header with response metadata including namespace, name, and message ID
        /// </summary>
        [JsonProperty("header")]
        public SmartHomeHeader Header { get; set; }

        /// <summary>
        /// Optional endpoint information identifying the device
        /// </summary>
        [JsonProperty("endpoint", NullValueHandling = NullValueHandling.Ignore)]
        public SmartHomeEndpoint Endpoint { get; set; }

        /// <summary>
        /// Response payload containing operation-specific data
        /// </summary>
        [JsonProperty("payload")]
        public object Payload { get; set; }
    }
}
