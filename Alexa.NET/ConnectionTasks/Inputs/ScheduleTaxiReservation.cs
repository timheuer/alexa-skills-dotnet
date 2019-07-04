using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Helpers;
using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class ScheduleTaxiReservation:IConnectionTask
    {
        public const string AssociatedUri = "connection://AMAZON.ScheduleTaxiReservation/1";
        [JsonIgnore]
        public string ConnectionUri => AssociatedUri;

        [JsonProperty("@type")]
        public string Type => "ScheduleTaxiReservationRequest";

        [JsonProperty("@version")]
        public string Version => 1.ToString();

        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public ConnectionTaskContext Context { get; set; }

        [JsonProperty("partySize")]
        public int PartySize { get; set; }

        [JsonProperty("pickupLocation",NullValueHandling = NullValueHandling.Ignore)]
        public PostalAddress PickupLocation { get; set; }

        [JsonProperty("dropoffLocation",NullValueHandling = NullValueHandling.Ignore)]
        public PostalAddress DropoffLocation { get; set; }

        [JsonProperty("pickupTime",NullValueHandling = NullValueHandling.Ignore),JsonConverter(typeof(MixedDateTimeConverter))]
        public DateTime? PickupTime { get; set; }
    }
}
