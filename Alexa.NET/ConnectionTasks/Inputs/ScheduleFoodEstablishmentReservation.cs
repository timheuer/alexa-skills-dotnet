using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Helpers;
using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class ScheduleFoodEstablishmentReservation:IConnectionTask
    {
        public const string AssociatedUri = "connection://AMAZON.ScheduleFoodEstablishmentReservation/1";
        [JsonIgnore]
        public string ConnectionUri => AssociatedUri;

        [JsonProperty("@type")]
        public string Type => "ScheduleFoodEstablishmentReservationRequest";

        [JsonProperty("@version")]
        public string Version => 1.ToString();

        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public ConnectionTaskContext Context { get; set; }

        [JsonProperty("partySize",NullValueHandling = NullValueHandling.Ignore)]
        public int PartySize { get; set; }

        [JsonProperty("startTime",NullValueHandling = NullValueHandling.Ignore),JsonConverter(typeof(MixedDateTimeConverter))]
        public DateTime? StartTime { get; set; }

        [JsonProperty("restaurant")]
        public Restaurant Restaurant { get; set; }
    }
}
