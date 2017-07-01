using System;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive.Templates
{
    public class TextContent
    {
        [JsonProperty("primaryText")]
        public TemplateText PrimaryText { get; set; }

        [JsonProperty("secondaryText")]
        public TemplateText SecondaryText { get; set; }

        [JsonProperty("tertiaryText")]
        public TemplateText TertiaryText { get; set; }
    }
}
