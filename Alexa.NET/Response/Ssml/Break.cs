using System;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
    public class Break:IParagraphSsml,ISentenceSsml
    {
        public XNode ToXml()
        {
            return new XElement("break");
        }
    }
}
