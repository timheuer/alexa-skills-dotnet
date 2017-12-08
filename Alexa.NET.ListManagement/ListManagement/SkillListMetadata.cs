using System.Collections.Generic;
using Alexa.NET.ListManagement.Requests;
using Newtonsoft.Json;

namespace Alexa.NET.ListManagement
{
    public class SkillListMetadata
    {
        [JsonProperty("listId")]
        public string ListId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("statusMap"),JsonConverter(typeof(JsonStatusMapConverter))]
        public IDictionary<string,string> StatusMap { get; set; }
    }
}
