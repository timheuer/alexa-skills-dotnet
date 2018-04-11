using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Alexa.NET.Response.Ssml
{
    public class Sentence:IParagraphSsml
    {
        public Sentence(){}
        public Sentence(string text):this(new PlainText(text)){
        }

        public Sentence(params ISentenceSsml[] elements)
        {
            Elements = elements.ToList();
        }

    public List<ISentenceSsml> Elements { get; set; } = new List<ISentenceSsml>();

        public XNode ToXml()
        {
            return new XElement("s",Elements.Select(e => e.ToXml()));
        }
    }
}
