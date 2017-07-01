using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public interface IListTemplate : ITemplate
    {
        [JsonProperty("listItems", Required = Required.Always)]
        List<ListItem> Items { get; set; }
    }
}
