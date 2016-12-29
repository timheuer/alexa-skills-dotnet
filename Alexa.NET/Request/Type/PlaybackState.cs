using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET.Request.Type
{
    public class PlaybackState
    {
        [JsonProperty("token")]
        string Token { get; set; }

        [JsonProperty("offsetInMilliseconds")]
        string OffsetInMilliseconds { get; set; }

        [JsonProperty("playerActivity")]
        string PlayerActivity { get; set; }
    }
}
