using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class SendToPhoneResult
    {
        [JsonProperty("directLaunch",NullValueHandling = NullValueHandling.Ignore)]
        public STPDirectLaunchResult DirectLaunch { get; set; }

        [JsonProperty("sendToDevice", NullValueHandling = NullValueHandling.Ignore)]
        public STPLinkResult SendToDevice { get; set; }
    }
}