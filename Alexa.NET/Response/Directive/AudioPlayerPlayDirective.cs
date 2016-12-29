using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Response.Directive
{
    public class AudioPlayerPlayDirective : IDirective
    {
        [JsonProperty("playBehavior")]
        [JsonRequired]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlayBehavior PlayBehavior { get; set; }

        [JsonProperty("audioItem")]
        [JsonRequired]
        public AudioItem AudioItem { get; set; }

        [JsonProperty("type")]
        public string Type => "AudioPlayer.Play";
    }
}
