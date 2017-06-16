using System;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class DialogDelegate:IDirective
    {
        [JsonProperty("type")]
        public string Type => "Dialog.Delegate";

        [JsonProperty("updatedIntent",NullValueHandling = NullValueHandling.Ignore)]
        public Intent UpdatedIntent { get; set; }
    }
}
