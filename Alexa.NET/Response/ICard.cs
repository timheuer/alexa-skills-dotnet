using Alexa.NET.Response.Converters;
using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    [JsonConverter(typeof(CardConverter))]
    public interface ICard : IResponse
    {
    }
}