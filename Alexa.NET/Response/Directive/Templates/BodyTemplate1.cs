using System;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive.Templates
{
    public class BodyTemplate1:IBodyTemplate
    {
        public string Type { get; set; }
        public string Token { get; set; }

        [JsonProperty("backButton")]
        public string BackButton { get; set; }

        [JsonProperty("backgroundImage")]
        public TemplateImage BackgroundImage { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("textContent")]
        public TemplateText Content { get; set; }
    }
}
