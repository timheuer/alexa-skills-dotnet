using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class AskForPermissionPayload
    {
        public AskForPermissionPayload()
        {

        }

        public AskForPermissionPayload(string permissionScope)
        {
            PermissionScope = permissionScope;
        }

        [JsonProperty("@type")] 
        public string Type { get; set; } = "AskForPermissionsConsentRequest";

        [JsonProperty("@version")] 
        public string Version { get; set; } = "1";

        [JsonProperty("permissionScope")]
        public string PermissionScope { get; set; }
    }
}