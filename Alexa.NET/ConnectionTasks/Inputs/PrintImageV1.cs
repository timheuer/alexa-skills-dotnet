using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class PrintImageV1 : IConnectionTask
    {
        public const string AssociatedUri = "connection://AMAZON.PrintImage/1";
        [JsonIgnore]
        public string ConnectionUri => AssociatedUri;

        [JsonProperty("@type")]
        public string Type => "PrintImageRequest";

        [JsonProperty("@version")]
        public string Version => 1.ToString();

        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public ConnectionTaskContext Context { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("imageType"),JsonConverter(typeof(StringEnumConverter))]
        public PrintImageV1Type ImageV1Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public enum PrintImageV1Type
    {
        JPG,
        JPEG
    }
}
