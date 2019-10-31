using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Helpers;
using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class SkillEventRequest:Request
    {
        [JsonProperty("eventCreationTime", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(MixedDateTimeConverter))]
        public DateTime? EventCreationTime { get; set; }

        [JsonProperty("eventPublishingTime", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(MixedDateTimeConverter))]
        public DateTime? EventPublishingTime { get; set; }
    }
}
