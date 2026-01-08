using Newtonsoft.Json;

namespace Alexa.NET.Request.SmartHome
{
    /// <summary>
    /// Device endpoint identification and authorization scope for Smart Home directives
    /// </summary>
    public class SmartHomeEndpoint
    {
        /// <summary>
        /// Unique identifier for the device endpoint
        /// </summary>
        [JsonProperty("endpointId", NullValueHandling = NullValueHandling.Ignore)]
        public string EndpointId { get; set; }

        /// <summary>
        /// Authorization scope containing access token for the user
        /// </summary>
        [JsonProperty("scope", NullValueHandling = NullValueHandling.Ignore)]
        public Scope Scope { get; set; }

        /// <summary>
        /// Optional custom data associated with the endpoint
        /// </summary>
        [JsonProperty("cookie", NullValueHandling = NullValueHandling.Ignore)]
        public object Cookie { get; set; }
    }
}
