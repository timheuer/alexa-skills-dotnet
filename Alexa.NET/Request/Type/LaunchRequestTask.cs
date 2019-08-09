using Alexa.NET.ConnectionTasks;
using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class LaunchRequestTask
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("input")]
        public IConnectionTask Input { get; set; }
    }
}