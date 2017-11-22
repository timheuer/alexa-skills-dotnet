using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public class ProgressiveResponseRequest
    {
        public ProgressiveResponseRequest()
        {
        }

        public ProgressiveResponseRequest(ProgressiveResponseHeader header, IProgressiveResponseDirective directive)
        {
            Header = header;
            Directive = directive;
        }

        [JsonProperty("header")]
        public ProgressiveResponseHeader Header { get; set; }

        [JsonProperty("directive")]
        public IProgressiveResponseDirective Directive { get; set; }
    }
}
