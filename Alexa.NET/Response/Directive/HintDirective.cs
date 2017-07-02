using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Alexa.NET.Response.Directive
{
    public class HintDirective:IDirective
    {
        [JsonProperty("type")]
        public string Type => "Hint";
        
        [JsonProperty("hint")]
        public Hint Hint { get; set; }
    }
}
