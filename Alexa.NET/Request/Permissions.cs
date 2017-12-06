using System;
using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Permissions
    {
        [JsonProperty("consentToken"),Obsolete("ConsentToken is deprecated, please use SkillRequest.Context.System.ApiAccessToken")]
        public string ConsentToken { get; set; }
    }
}