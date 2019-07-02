using System;
using Alexa.NET.Response.Directive.ConnectionTasks;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class StartConnectionDirective:IDirective
    {
        [JsonProperty("type")]
        public string Type => "Connections.StartConnection";

        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("input")]
        public IConnectionTask Input { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        public StartConnectionDirective(){}

        public StartConnectionDirective(IConnectionTask input, string token)
        {
            this.Input = input;
            this.Token = token;
        }
    }
}
