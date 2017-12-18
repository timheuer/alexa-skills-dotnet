using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.ListManagement
{
    public class SkillList
    {
        [JsonProperty("listId")]
        public string ListId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("links")]
        public Dictionary<string, string> Links { get; set; }

        [JsonProperty("items")]
        public List<SkillListItem> Items { get; set; } = new List<SkillListItem>();
    }
}
