using System.Text;
using Alexa.NET.Response.Converters;
using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks
{
    [JsonConverter(typeof(ConnectionTaskConverter))]
    public interface IConnectionTask
    {
        [JsonIgnore]
        string ConnectionUri { get; }
    }
}
