using Alexa.NET.Response.Converters;
using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    [JsonConverter(typeof(OutputSpeechConverter))]
    public interface IOutputSpeech : IResponse
    {
    }
}