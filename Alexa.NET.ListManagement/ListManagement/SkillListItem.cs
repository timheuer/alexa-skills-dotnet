using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.ListManagement.Requests;
using Newtonsoft.Json;

namespace Alexa.NET.ListManagement
{
    public class SkillListItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("createdTime"), JsonConverter(typeof(SkillItemDateConverter))]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("updatedTime"), JsonConverter(typeof(SkillItemDateConverter))]
        public DateTime UpdatedTime { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
