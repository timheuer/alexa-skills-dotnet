using Alexa.NET.Response.Directive;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Response
{
    public class PlainTextOutputSpeech : IOutputSpeech
    {
        [JsonProperty("type")]
        [JsonRequired]
        public string Type
        {
            get { return "PlainText"; }
        }

        [JsonRequired]
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("playBehavior", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlayBehavior? PlayBehavior { get; set; }
    }
}