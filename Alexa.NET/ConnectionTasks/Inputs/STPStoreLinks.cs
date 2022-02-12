using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPStoreLinks
    {
        [JsonProperty("GOOGLE_PLAY_STORE",NullValueHandling = NullValueHandling.Ignore)]
        public STPLinkDetails GooglePlayStore { get; set; }

        [JsonProperty("IOS_APP_STORE",NullValueHandling = NullValueHandling.Ignore)]
        public STPLinkDetails IOSAppStore { get; set; }
    }
}