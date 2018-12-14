using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class LocationServices
    {
        [JsonProperty("access")]
        public LocationServiceAccess Access { get; set; }

        [JsonProperty("status")]
        public LocationServiceStatus Status { get; set; }
    }
}
