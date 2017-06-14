using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Alexa.NET.Response.Ssml
{
    public class Sentence:IParagraphSsml
    {
        public Sentence(){}
        public Sentence(string text){
            Elements.Add(new PlainText(text));
        }

        public List<ISentenceSsml> Elements { get; set; } = new List<ISentenceSsml>();

        public XNode ToXml()
        {
            return new XElement("s",Elements.Select(e => e.ToXml()));
        }
    }
}
