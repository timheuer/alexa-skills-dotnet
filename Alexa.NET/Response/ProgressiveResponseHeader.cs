using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public class ProgressiveResponseHeader
    {
        public ProgressiveResponseHeader(string requestId)
        {
            RequestId = requestId;
        }

        [JsonProperty("requestId")]
        public string RequestId { get; }
    }
}
