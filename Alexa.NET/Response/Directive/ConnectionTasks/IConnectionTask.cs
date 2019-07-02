using System.Text;
using Alexa.NET.Response.Converters;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive.ConnectionTasks
{
    [JsonConverter(typeof(ConnectionTaskConverter))]
    public interface IConnectionTask
    {
        
    }
}
