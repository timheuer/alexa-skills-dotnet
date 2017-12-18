using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.ListManagement
{
    public class GetListResponse
    {
        [JsonProperty("Lists")]
        public List<SkillListMetadata> Lists { get; set; }
    }
}
