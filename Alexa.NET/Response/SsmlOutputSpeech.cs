using Alexa.NET.Response.Directive;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

        [JsonProperty("playBehavior", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlayBehavior? PlayBehavior { get; set; }
    }
}