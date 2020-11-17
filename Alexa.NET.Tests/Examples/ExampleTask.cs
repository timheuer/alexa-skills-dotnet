using Alexa.NET.ConnectionTasks;
using Newtonsoft.Json;

namespace Alexa.NET.Tests.Examples
{
    public class ExampleTask : IConnectionTask
    {
        public string ConnectionUri { get; set; }
        [JsonProperty("randomParameter")]
        public string RandomParameter { get; set; }
    }
}
