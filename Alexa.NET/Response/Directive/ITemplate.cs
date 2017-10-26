using System;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public interface ITemplate
    {
        [JsonProperty("type", Required = Required.Always)]
        string Type { get; }

        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        string Token { get; set; }
    }
}
