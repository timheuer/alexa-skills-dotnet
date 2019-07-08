using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks
{
    public class ConnectionTaskContext
    {
        [JsonProperty("providerId")]
        public string ProviderId { get; set; }
    }
}