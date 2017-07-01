using System;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive.Templates
{
    public class TemplateContent
    {
        [JsonProperty("primaryText")]
        public TemplateText Primary { get; set; }

        [JsonProperty("secondaryText")]
        public TemplateText Secondary { get; set; }

        [JsonProperty("tertiaryText")]
        public TemplateText Tertiary { get; set; }
    }
}
