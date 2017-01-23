using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public interface IResponse
    {
        [JsonRequired]
        string Type { get; }
    }
}
