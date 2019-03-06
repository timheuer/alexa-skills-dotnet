using Newtonsoft.Json;
using System.Collections.Generic;
using Alexa.NET.Response.Converters;

namespace Alexa.NET.Response
{
    public class ResponseBody
    {
        [JsonProperty("outputSpeech", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(OutputSpeechConverter))]
        public IOutputSpeech OutputSpeech { get; set; }

        [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CardConverter))]
        public ICard Card { get; set; }

        [JsonProperty("reprompt", NullValueHandling = NullValueHandling.Ignore)]
        public Reprompt Reprompt { get; set; }

        [JsonProperty("shouldEndSession", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShouldEndSession { get; set; } = false;

        [JsonProperty("directives", NullValueHandling = NullValueHandling.Ignore, ItemConverterType = typeof(DirectiveConverter))]
        public IList<IDirective> Directives { get; set; } = new List<IDirective>();

        public bool ShouldSerializeDirectives()
        {
            return Directives.Count > 0;
        }
    }
}