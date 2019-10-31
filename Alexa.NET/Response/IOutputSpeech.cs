using Alexa.NET.Response.Converters;
using Alexa.NET.Response.Directive;
using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    [JsonConverter(typeof(OutputSpeechConverter))]
    public interface IOutputSpeech : IResponse
    {
        PlayBehavior? PlayBehavior { get; set; }
    }
}