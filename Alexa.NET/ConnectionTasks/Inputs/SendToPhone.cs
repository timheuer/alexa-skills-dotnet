using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    //https://developer.amazon.com/en-US/docs/alexa/alexa-for-apps/skill-connection-request-reference.html

    public class SendToPhone:IConnectionTask
    {
        public SendToPhone(){}

        public SendToPhone(STPStoreLinks links)
        {
            Links = links;
        }

        public const string AssociatedUri = "connection://AMAZON.LinkApp/2";

        [JsonIgnore]
        public string ConnectionUri => AssociatedUri;

        [JsonProperty("links")] public STPStoreLinks Links { get; } = new STPStoreLinks();

        [JsonProperty("prompt",NullValueHandling = NullValueHandling.Ignore)]
        public STPPrompt Prompt { get; set; }

        [JsonProperty("directLaunch",NullValueHandling = NullValueHandling.Ignore)]
        public STPDirectLaunch DirectLaunch { get; set; }
    }
}