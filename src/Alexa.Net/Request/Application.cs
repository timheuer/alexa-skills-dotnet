using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Application
    {
        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; }
    }
}
