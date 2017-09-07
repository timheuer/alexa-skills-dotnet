using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class AccountLinkSkillEventRequest:Request
    {
        [JsonProperty("body")]
        public AccountLinkSkillEventDetail Body { get; set; }
    }

    public class AccountLinkSkillEventDetail
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
    }
}
