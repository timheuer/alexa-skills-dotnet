using System;
using Alexa.NET.Request;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class DialogConfirmSlot: IDirective
    {
        [JsonProperty("type")]
        public string Type => "Dialog.ConfirmSlot";

        [JsonProperty("slotToConfirm"),JsonRequired]
        public string SlotName { get; set; }

		[JsonProperty("updatedIntent", NullValueHandling = NullValueHandling.Ignore)]
		public Intent UpdatedIntent { get; set; }

        public DialogConfirmSlot(string slotName)
        {
            SlotName = slotName;
        }
    }
}
