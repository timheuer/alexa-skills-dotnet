using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
    public class Lang:ICommonSsml
    {
        public string LanguageCode { get; set; }

        public List<ISsml> Elements { get; set; } = new List<ISsml>();

        public Lang(string languageCode)
        {
            LanguageCode = languageCode ?? throw new ArgumentNullException(nameof(languageCode));
        }

        public Lang(string languageCode, params ISsml[] elements)
        {
            LanguageCode = languageCode ?? throw new ArgumentNullException(nameof(languageCode));
            Elements = elements.ToList();
        }

        public XNode ToXml()
        {
            return new XElement("lang", new XAttribute(XNamespace.Xml + "lang", LanguageCode), Elements.Select(e => e.ToXml()));
        }
    }
}
