using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive.Templates
{
    public class TemplateImage
    {
        [JsonProperty("contentDescription", Required = Required.Always)]
        public string ContentDescription { get; set; }

        [JsonProperty("sources")]
        public List<ImageSource> Sources {get;set;} = new List<ImageSource>();
    }
}