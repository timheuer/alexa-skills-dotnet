using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Alexa.NET.ListManagement.Requests
{
    internal class SkillListUpdateRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }
    }
}
