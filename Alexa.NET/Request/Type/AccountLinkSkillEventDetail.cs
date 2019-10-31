using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class AccountLinkSkillEventDetail
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
    }
}