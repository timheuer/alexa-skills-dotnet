using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET.Response.Directive
{
    public class StopDirective : IDirective
    {
        [JsonProperty("type")]
        public string Type => "AudioPlayer.Stop";
    }
}
