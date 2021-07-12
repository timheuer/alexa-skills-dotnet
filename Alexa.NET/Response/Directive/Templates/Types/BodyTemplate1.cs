using System;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive.Templates.Types
{
    [Obsolete("Display Templates are deprecated as of August 31st 2021. For more information visit https://developer.amazon.com/en-US/blogs/alexa/alexa-skills-kit/2021/06/-goodbye-display-templates--hello-alexa-responsive-templates")]
    public class BodyTemplate1:IBodyTemplate
    {
        public string Type => "BodyTemplate1";
        public string Token { get; set; }

        [JsonProperty("backButton",NullValueHandling = NullValueHandling.Ignore)]
        public string BackButton { get; set; }

        [JsonProperty("backgroundImage",NullValueHandling = NullValueHandling.Ignore)]
        public TemplateImage BackgroundImage { get; set; }

        [JsonProperty("title",NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("textContent")]
        public TemplateContent Content { get; set; }
    }
}
