using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Permissions
    {
        [JsonProperty("consentToken"),Obsolete("ConsentToken is deprecated, please use SkillRequest.Context.System.ApiAccessToken")]
        public string ConsentToken { get; set; }

        [JsonProperty("scopes", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, Scope> Scopes { get; set; }
    }
}