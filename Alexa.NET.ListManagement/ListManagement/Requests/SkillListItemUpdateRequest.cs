using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.ListManagement.Requests
{
    internal class SkillListItemUpdateRequest
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }
    }
}
