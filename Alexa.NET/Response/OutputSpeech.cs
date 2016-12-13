using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public class PlainTextOutputSpeech : IOutputSpeech
    {
        /// <summary>
        /// A string containing the type of output speech to render. Valid types are:
        /// - "PlainText" - Indicates that the output speech is defined as plain text.
        /// - "SSML" - Indicates that the output speech is text marked up with SSML.
        /// </summary>
        [JsonProperty("type")]
        [JsonRequired]
        public string Type
        {
            get { return "PlainText"; }
        }

        /// <summary>
        /// A string containing the speech to render to the user. Use this when type is "PlainText"
        /// </summary>
        [JsonRequired]
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}