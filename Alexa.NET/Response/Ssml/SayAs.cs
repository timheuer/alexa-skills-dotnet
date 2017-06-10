using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
    public class SayAs:IParagraphSsml,ISentenceSsml
    {
        public string Text { get; set; }
        public string InterpretAs { get; set; }
        public string Format { get; set; }

        public SayAs(string text, string interpretAs)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(nameof(text), "Text value required for SayAs in Ssml");
            }

			if (string.IsNullOrWhiteSpace(interpretAs))
			{
				throw new ArgumentNullException(nameof(interpretAs), "InterpretAs value required for SayAs in Ssml");
			}

            Text = text;
            InterpretAs = interpretAs;
        }

        public XNode ToXml()
        {
            List<XObject> attributes = new List<XObject>();

            attributes.Add(new XText(Text));
            attributes.Add(new XAttribute("interpret-as", InterpretAs));

            if(!string.IsNullOrWhiteSpace(Format))
            {
                attributes.Add(new XAttribute("format", Format));
            }

            return new XElement("say-as", attributes);
        }
    }
}
