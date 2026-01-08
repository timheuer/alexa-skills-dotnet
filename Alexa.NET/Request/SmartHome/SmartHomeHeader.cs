using Newtonsoft.Json;

namespace Alexa.NET.Request.SmartHome
{
    /// <summary>
    /// Header metadata for Smart Home directives including namespace, name, and message identification
    /// </summary>
    public class SmartHomeHeader
    {
        /// <summary>
        /// The namespace of the directive (e.g., Alexa.Discovery, Alexa.PowerController)
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// The name of the directive (e.g., Discover, TurnOn, TurnOff)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// A unique identifier for this message
        /// </summary>
        [JsonProperty("messageId")]
        public string MessageId { get; set; }

        /// <summary>
        /// The version of the Smart Home API (should be "3" for v3)
        /// </summary>
        [JsonProperty("payloadVersion")]
        public string PayloadVersion { get; set; }

        /// <summary>
        /// A correlation token used to match responses to requests
        /// </summary>
        [JsonProperty("correlationToken", NullValueHandling = NullValueHandling.Ignore)]
        public string CorrelationToken { get; set; }
    }
}
