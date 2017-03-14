using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET.Request.Type
{
    public class ErrorCause
    {
        [JsonProperty("requestId")]
        public string requestId { get; set; }
    }
}
