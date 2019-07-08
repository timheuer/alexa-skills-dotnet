using System;
using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class PrintWebPageV1:IConnectionTask
    {
        public const string AssociatedUri = "connection://AMAZON.PrintWebPage/1";

        [JsonIgnore]
        public string ConnectionUri => AssociatedUri;

        [JsonProperty("@type")]
        public string Type => "PrintWebPageRequest";

        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public ConnectionTaskContext Context { get; set; }

        [JsonProperty("@version")]
        public string Version => 1.ToString();

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

    }
}
