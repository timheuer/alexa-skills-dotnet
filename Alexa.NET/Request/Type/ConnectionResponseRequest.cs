using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class ConnectionResponseRequest<T> : ConnectionResponseRequest
    {
        [JsonProperty("payload")]
        public T Payload { get; set; }
    }

    public class ConnectionResponseRequest:Request
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public ConnectionStatus Status { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}