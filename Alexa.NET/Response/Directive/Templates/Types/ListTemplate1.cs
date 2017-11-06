using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive.Templates.Types
{
    public class ListTemplate1:IListTemplate
    {
        public string Type => "ListTemplate1";
        public string Token { get; set; }

        [JsonProperty("backButton", NullValueHandling = NullValueHandling.Ignore)]
        public string BackButton { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("backgroundImage", NullValueHandling = NullValueHandling.Ignore)]
        public TemplateImage BackgroundImage { get; set; }

        public List<ListItem> Items { get; set; } = new List<ListItem>();

        public bool ShouldSerializeItems()
        {
            return Items.Count > 0;
        }
    }
}
