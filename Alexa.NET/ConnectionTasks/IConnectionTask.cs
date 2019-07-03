using System.Text;
using Alexa.NET.Response.Converters;
using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks
{
    [JsonConverter(typeof(ConnectionTaskConverter))]
    public interface IConnectionTask
    {
        [JsonProperty("@type")]
        string Type { get; }

        [JsonProperty("@version")]
        int Version { get; }

        [JsonProperty("context",NullValueHandling = NullValueHandling.Ignore)]
        ConnectionTaskContext Context { get; set; }
    }
}
