using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Permissions
    {
        [JsonProperty("consentToken")]
        public string ConsentToken { get; set; }
    }
}