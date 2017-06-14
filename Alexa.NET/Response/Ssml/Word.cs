using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
    public class Word:ICommonSsml
    {
        public string Text { get; set; }
        public string Role { get; set; }

        public Word(string text, string role)
        {
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentNullException(nameof(text), "Text value required for Word in Ssml");
			}

            if(string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentNullException(nameof(text), "Role value required for Word in Ssml");
            }

            Text = text;
            Role = role;
        }

        public XNode ToXml()
        {
            List<XObject> objects = new List<XObject>();

            objects.Add(new XAttribute("role", Role));
            objects.Add(new XText(Text));

            return new XElement("w", objects);
        }
    }
}
