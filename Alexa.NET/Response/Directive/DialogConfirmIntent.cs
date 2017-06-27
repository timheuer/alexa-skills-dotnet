using System;
using Alexa.NET.Request;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class DialogConfirmIntent : IDirective
    {
        [JsonProperty("type")]
        public string Type => "Dialog.ConfirmIntent";

        [JsonProperty("updatedIntent", NullValueHandling = NullValueHandling.Ignore)]
        public Intent UpdatedIntent { get; set; }
    }
}
