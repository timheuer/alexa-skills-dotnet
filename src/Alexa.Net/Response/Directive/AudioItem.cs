﻿using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class AudioItem
    {
        [JsonRequired]
        [JsonProperty("stream")]
        public AudioItemStream Stream { get; set; }
    }
}
