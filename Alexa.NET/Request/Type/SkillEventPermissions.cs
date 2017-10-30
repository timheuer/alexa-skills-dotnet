using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class SkillEventPermissions
    {
        [JsonProperty("acceptedPermissions")]
        public Permission[] AcceptedPermissions { get; set; }
    }
}