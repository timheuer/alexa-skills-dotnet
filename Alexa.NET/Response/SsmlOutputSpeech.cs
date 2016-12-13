using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    class SsmlOutputSpeech : IOutputSpeech
    {
        [JsonRequired]
        [JsonProperty("type")]
        public string Type
        {
            get { return "SSML"; }
        }

        [JsonRequired]
        [JsonProperty("ssml")]
        public string Ssml { get; set; }
    }
}