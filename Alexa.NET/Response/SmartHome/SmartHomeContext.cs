using Newtonsoft.Json;
using System.Collections.Generic;

namespace Alexa.NET.Response.SmartHome
{
    public class SmartHomeContext
    {
        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public List<SmartHomeProperty> Properties { get; set; }
    }
}
