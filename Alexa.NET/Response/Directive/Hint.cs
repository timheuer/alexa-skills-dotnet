using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Alexa.NET.Response.Directive
{
    public class Hint
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
