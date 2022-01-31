using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public class ResponseExperimentation
    {
        public ResponseExperimentation()
        {

        }

        public ResponseExperimentation(params string[] experimentIds)
        {
            TriggeredExperiments = experimentIds.ToList();
        }

        [JsonProperty("triggeredExperiments")]
        public List<string> TriggeredExperiments { get; set; } = new List<string>();
    }
}