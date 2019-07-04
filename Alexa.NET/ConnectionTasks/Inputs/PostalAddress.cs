using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class PostalAddress
    {
        [JsonProperty("@type")] public string Type => "PostalAddress";
        [JsonProperty("@version")] public string Version = 1.ToString();
        [JsonProperty("streetAddress")] public string StreetAddress { get; set; }
        [JsonProperty("locality")] public string Locality { get; set; }
        [JsonProperty("region")] public string Region { get; set; }
        [JsonProperty("postalCode")] public string PostalCode { get; set; }
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)] public string Country { get; set; }
    }
}
