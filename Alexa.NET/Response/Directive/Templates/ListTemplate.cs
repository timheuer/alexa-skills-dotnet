using System;
using System.Collections.Generic;

namespace Alexa.NET.Response.Directive.Templates
{
    public abstract class ListTemplate:IListTemplate
    {
		public string Type { get; set; }
		public string Token { get; set; }
        public List<ListItem> Items { get; set; } = new List<ListItem>();

        public bool ShouldSerializeItems()
        {
            return Items.Count > 0;
        }
    }
}
