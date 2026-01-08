using Newtonsoft.Json;

namespace Alexa.NET.Request.SmartHome
{
    /// <summary>
    /// Top-level wrapper for Alexa Smart Home directives sent from the Alexa service
    /// </summary>
    public class SmartHomeRequest
    {
        /// <summary>
        /// The directive containing header, endpoint, and payload information
        /// </summary>
        [JsonProperty("directive")]
        public SmartHomeDirective Directive { get; set; }
    }
}
