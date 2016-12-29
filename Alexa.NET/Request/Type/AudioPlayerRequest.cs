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
        string Token { get; set; }

        [JsonProperty("locale")]
        string Locale { get; set; }

        [JsonProperty("offsetInMilliseconds")]
        string OffsetInMilliseconds { get; set; }

        [JsonProperty("error")]
        Error Error { get; set; }

        [JsonProperty("currentPlaybackState")]
        PlaybackState CurrentPlaybackState { get; set; }
    }
}
