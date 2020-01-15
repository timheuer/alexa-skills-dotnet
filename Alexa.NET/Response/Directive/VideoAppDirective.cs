using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class VideoAppDirective:IEndSessionDirective
    {
        public VideoAppDirective()
        {
        }

        public VideoAppDirective(string source)
        {
            VideoItem = new VideoItem(source);
        }

        [JsonProperty("type")]
        public string Type => "VideoApp.Launch";

        [JsonProperty("videoItem",Required = Required.Always)]
        public VideoItem VideoItem { get; set; }

        public bool? ShouldEndSession => null;
    }
}
