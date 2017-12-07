using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.ListManagement
{
    internal class SkillListItemCreateRequest
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
