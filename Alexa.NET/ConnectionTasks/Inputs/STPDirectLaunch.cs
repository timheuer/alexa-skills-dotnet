using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPDirectLaunch
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}