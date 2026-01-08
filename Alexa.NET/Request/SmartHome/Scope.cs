using Newtonsoft.Json;

namespace Alexa.NET.Request.SmartHome
{
    /// <summary>
    /// Authorization scope structure containing bearer token for user authentication
    /// </summary>
    public class Scope
    {
        /// <summary>
        /// Type of authorization token (typically "BearerToken")
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// OAuth2.0 bearer token for user authentication
        /// </summary>
        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        /// <summary>
        /// Partition identifier for the user
        /// </summary>
        [JsonProperty("partition", NullValueHandling = NullValueHandling.Ignore)]
        public string Partition { get; set; }

        /// <summary>
        /// User identifier
        /// </summary>
        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }
    }
}
