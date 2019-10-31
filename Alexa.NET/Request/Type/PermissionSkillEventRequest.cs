using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class PermissionSkillEventRequest:SkillEventRequest
    {
        [JsonProperty("body")]
        public SkillEventPermissions Body { get; set; }
    }
}
