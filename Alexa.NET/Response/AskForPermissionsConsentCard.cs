using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Alexa.NET.Response
{
    public class AskForPermissionsConsentCard : ICard
    {

        [JsonProperty("type")]
        [JsonRequired]
        public string Type
        {
            get { return "AskForPermissionsConsent"; }
        }

        [JsonProperty("permissions")]
        [JsonRequired]
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
