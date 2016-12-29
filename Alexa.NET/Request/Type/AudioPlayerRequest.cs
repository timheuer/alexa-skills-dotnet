using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET.Request.Type
{
    public class AudioPlayerRequest: Request
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("offsetInMilliseconds")]
        public string OffsetInMilliseconds { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }

        [JsonProperty("currentPlaybackState")]
        public PlaybackState CurrentPlaybackState { get; set; }
    }
}
