using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class VoicePlayerSpeakDirective:IProgressiveResponseDirective
    {
        public VoicePlayerSpeakDirective(Ssml.Speech speech):this(speech.ToXml())
        {

        }

        public VoicePlayerSpeakDirective(string speech)
        {
            Speech = speech;
        }

        [JsonProperty("type")]
        public string Type => "VoicePlayer.Speak";

        [JsonProperty("speech")]
        public string Speech { get; }
    }
}
