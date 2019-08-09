using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks
{
    public class TaskStatus
    {
        public TaskStatus() { }

        public TaskStatus(int code, string message)
        {
            Code = code;
            Message = message;
        }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}