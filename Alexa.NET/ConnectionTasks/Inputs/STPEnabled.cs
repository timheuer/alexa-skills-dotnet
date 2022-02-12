using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPEnabled
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}