using System;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
	public class AmazonEffect : ICommonSsml
	{
		public string Text { get; set; }

		public AmazonEffect(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentNullException(nameof(text), "Text value required for AmazonEffect in Ssml");
			}

			Text = text;
		}

		public XNode ToXml()
		{
			XNamespace ns = "http://alexa.amazon.com";
			return new XElement(ns + "effect", new XAttribute(XNamespace.Xmlns + "amazon", ns), new XAttribute("name", "whispered"),new XText(Text));
		}
	}
}
