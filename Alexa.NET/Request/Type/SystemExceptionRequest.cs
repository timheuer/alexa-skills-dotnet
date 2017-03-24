using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET.Request.Type
{
    public class SystemExceptionRequest : Request
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
        [JsonProperty("cause")]
        public ErrorCause ErrorCause { get; set; }
    }
}
