using Alexa.NET.Response.Converters;
using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    [JsonConverter(typeof(DirectiveConverter))]
    public interface IDirective
    {
        [JsonRequired]
        string Type { get; }
    }
}