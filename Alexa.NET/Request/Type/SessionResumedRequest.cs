using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Request.Type
{
    public class SessionResumedRequest:Request
    {
        [JsonProperty("originIpAddress")]
        public string OriginIpAddress { get; set; }

        [JsonProperty("cause")]
        public SessionResumedRequestCause Cause { get; set; }
    }
}
