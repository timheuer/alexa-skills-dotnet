using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Alexa.NET.Response.Ssml
{
    public class Paragraph : ISsml
    {
        public List<IParagraphSsml> Elements {get;set;} = new List<IParagraphSsml>();

        public Paragraph() { }

        public Paragraph(params IParagraphSsml[] elements)
        {
            Elements = elements.ToList();
        }

        public XNode ToXml()
        {
            return new XElement("p", Elements.Select(e => e.ToXml()));
        }
    }
}
