using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public class SsmlOutputSpeech : IOutputSpeech
    {
        public SsmlOutputSpeech()
        {

        }

        public SsmlOutputSpeech(string ssml)
        {
            Ssml = ssml;
        }

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