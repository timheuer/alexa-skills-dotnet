using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public class PlainTextOutputSpeech : IOutputSpeech
    {
        public PlainTextOutputSpeech()
        {

        }

        public PlainTextOutputSpeech(string text)
        {
            Text = text;
        }

        [JsonProperty("type")]
        [JsonRequired]
        public string Type
        {
            get { return "PlainText"; }
        }

        [JsonRequired]
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}