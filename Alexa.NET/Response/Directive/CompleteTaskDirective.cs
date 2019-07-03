using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.ConnectionTasks;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class CompleteTaskDirective:IDirective
    {
        public string Type => "Tasks.CompleteTask";

        [JsonProperty("status")]
        public TaskStatus Status { get; set; } 
    }
}
