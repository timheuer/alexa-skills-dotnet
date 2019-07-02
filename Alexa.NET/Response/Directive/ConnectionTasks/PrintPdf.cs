using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive.ConnectionTasks
{
    public class PrintPdfV1:IConnectionTask
    {
        public const string AssociatedUri = "connection://AMAZON.PrintPDF/1";

        [JsonProperty("@type")]
        public string Type => "PrintPDFRequest";

        [JsonProperty("@version")]
        public int Version = 1;

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

    }
}
