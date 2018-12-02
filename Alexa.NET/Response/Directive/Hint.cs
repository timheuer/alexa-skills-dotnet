using Alexa.NET.Response.Directive.Templates;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class Hint
    {
        public Hint()
        {
        }

        public Hint(string hintText, string textType = TextType.Plain)
        {
            Text = hintText;
            Type = textType;
        }

        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
