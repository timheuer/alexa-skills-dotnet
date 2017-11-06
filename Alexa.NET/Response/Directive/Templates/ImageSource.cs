using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive.Templates
{
    public class ImageSource
    {
        [JsonProperty("url", Required = Required.Always)]
        public string Url { get; set; }

        [JsonProperty("size",NullValueHandling = NullValueHandling.Ignore)]
        public string Size { get; set; }

        [JsonProperty("widthPixels")]
        public int Width { get; set; }

        [JsonProperty("heightPixels")]
        public int Height { get; set; }

        public bool ShouldSerializeWidth()
        {
            return Width > 0;
        }

        public bool ShouldSerializeHeight()
        {
            return Height > 0;
        }
    }
}