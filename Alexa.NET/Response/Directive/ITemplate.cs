using System;
using Alexa.NET.Response.Converters;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    [JsonConverter(typeof(TemplateConverter))]
    public interface ITemplate
    {
        [JsonProperty("type", Required = Required.Always)]
        string Type { get; }

        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        string Token { get; set; }
    }
}