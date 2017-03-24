using Alexa.NET.Request.Type;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET.Request
{
    public class Context
    {
        [JsonProperty("System")]
        public AlexaSystem System { get; set; }
        
        [JsonProperty("AudioPlayer")]
        public PlaybackState AudioPlayer { get; set; }

    }
}
