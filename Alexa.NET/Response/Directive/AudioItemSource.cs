using System;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
	public class AudioItemSource
	{
		public AudioItemSource()
		{
		}

		public AudioItemSource(string url)
		{
			Url = url;
		}

		[JsonProperty("url"), JsonRequired]
		public string Url { get; set; }
    }
}
