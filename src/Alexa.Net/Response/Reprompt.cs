using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public class Reprompt
    {
        [JsonProperty("outputSpeech", NullValueHandling=NullValueHandling.Ignore)]
        public IOutputSpeech OutputSpeech { get; set; }
    }
}