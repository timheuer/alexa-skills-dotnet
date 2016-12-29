using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET.Request.Type
{
    public class Error
    {
        [JsonProperty("type")]
        string Type { get; set; }

        [JsonProperty("message")]
        string Message { get; set; }
    }
}
