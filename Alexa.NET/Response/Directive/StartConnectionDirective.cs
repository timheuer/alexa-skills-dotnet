using System;
using Alexa.NET.ConnectionTasks;
using Newtonsoft.Json;

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

        public StartConnectionDirective(){}

        public StartConnectionDirective(IConnectionTask input, string token)
        {
            this.Uri = input.ConnectionUri;
            this.Input = input;
            this.Token = token;
        }
    }
}
