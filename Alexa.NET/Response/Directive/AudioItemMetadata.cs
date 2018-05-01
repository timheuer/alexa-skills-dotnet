using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class AudioItemMetadata
    {
		[JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("subtitle")]
		public string Subtitle { get; set; }

		[JsonProperty("art")]
		public AudioItemSources Art { get; set; } = new AudioItemSources();

		[JsonProperty("backgroundImage")]
		public AudioItemSources BackgroundImage { get; set; } = new AudioItemSources();
    }
}