using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Alexa.NET.Response
{
    public class ResponseBody
    {
        [JsonProperty("outputSpeech", NullValueHandling = NullValueHandling.Ignore)]
        public IOutputSpeech OutputSpeech { get; set; }

        [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
        public ICard Card { get; set; }

        [JsonProperty("reprompt", NullValueHandling = NullValueHandling.Ignore)]
        public Reprompt Reprompt { get; set; }

        private bool? _shouldEndSession = false;

        [JsonProperty("shouldEndSession", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShouldEndSession
        {
            get
            {
                var overrideDirectives = Directives?.OfType<IEndSessionDirective>();
                if (overrideDirectives == null || !overrideDirectives.Any())
                {
                    return _shouldEndSession;
                }

                var first = overrideDirectives.First().ShouldEndSession;
                if (!overrideDirectives.All(od => od.ShouldEndSession == first))
                {
                    return _shouldEndSession;
                }

                return first;

            }
            set => _shouldEndSession = value;
        }

        [JsonProperty("directives", NullValueHandling = NullValueHandling.Ignore)]
        public IList<IDirective> Directives { get; set; } = new List<IDirective>();

        public bool ShouldSerializeDirectives()
        {
            return Directives.Count > 0;
        }
    }
}
