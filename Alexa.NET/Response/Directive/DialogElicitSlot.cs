using System;
using Alexa.NET.Request;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class DialogElicitSlot:IDirective
    {
		[JsonProperty("type")]
        public string Type => "Dialog.ElicitSlot";

        [JsonProperty("slotToElicit"),JsonRequired]
        public string SlotName { get; set; }

		[JsonProperty("updatedIntent", NullValueHandling = NullValueHandling.Ignore)]
		public Intent UpdatedIntent { get; set; }

        public DialogElicitSlot(string slotName)
        {
            SlotName = slotName;
        }
    }
}
