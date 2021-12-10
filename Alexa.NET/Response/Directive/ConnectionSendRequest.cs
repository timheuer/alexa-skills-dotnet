using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class ConnectionSendRequest<T> : ConnectionSendRequest
    {
        [JsonProperty("payload")]
        public T Payload { get; set; }
    }

    public class ConnectionSendRequest:IDirective
    {
        [JsonProperty("type")] public string Type => "Connections.SendRequest";

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("token")]
        string Token { get; set; }
    }
}