using System;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive.Templates
{
    public class TemplateText
    {
        [JsonProperty("text", Required = Required.Always)]
        public string Text { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }
    }
}
