using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET.Response.Directive
{
    public class ClearQueueDirective : IDirective
    {
        [JsonProperty("type")]
        public string Type => "AudioPlayer.ClearQueue";

        [JsonProperty("clearBehavior")]
        [JsonRequired]
        [JsonConverter(typeof(StringEnumConverter))]
        public ClearBehavior ClearBehavior { get; set; }
    }
}
