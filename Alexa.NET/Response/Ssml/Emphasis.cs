using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
    public class Emphasis:ICommonSsml
    {
        public string Text { get; set; }
        public string Level { get; set; }

        public Emphasis(string text)
        {
            Text = text;
        }

        public XNode ToXml()
        {
            List<XObject> objects = new List<XObject>();

            if (!string.IsNullOrWhiteSpace(Level))
            {
                objects.Add(new XAttribute("level", Level));
            }

            objects.Add(new XText(Text));

            return new XElement("emphasis", objects);
        }
    }
}
