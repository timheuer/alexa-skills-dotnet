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
            return new XElement(Namespaces.TempAmazon + "effect", new XAttribute("name", "whispered"),new XText(Text));
		}
	}
}
