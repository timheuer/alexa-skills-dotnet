using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
    public class Audio:ISsml
    {
        public string Source { get; set; }
        public List<ISsml> Elements { get; set; } = new List<ISsml>();

        public Audio() { }

        public Audio(params ISsml[] elements)
        {
            Elements = elements.ToList();
        }

        public Audio(string source)
        {
            if(string.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentNullException(nameof(source), "Source value required for Audio in Ssml");
            }

            Source = source;
        }

        public XNode ToXml()
        {
            return new XElement("audio", new XObject[]{new XAttribute("src",Source)}.Concat(Elements.Select(e => e.ToXml())));
        }
    }
}
