using System;
using Alexa.NET.ConnectionTasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Response.Directive
{
    public class StartConnectionDirective:IDirective
    {
        [JsonProperty("type")]
        public string Type => "Connections.StartConnection";

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("input")]
        public IConnectionTask Input { get; set; }

        [JsonProperty("token",NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty("onComplete",NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OnCompleteAction? OnComplete { get; set; }

        public StartConnectionDirective(){}

        public StartConnectionDirective(IConnectionTask input, string token)
        {
            this.Uri = input.ConnectionUri;
            this.Input = input;
            this.Token = token;
        }
    }
}
