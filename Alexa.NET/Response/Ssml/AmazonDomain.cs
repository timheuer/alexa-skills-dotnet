using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
    public class AmazonDomain : ICommonSsml
    {
        public string Name { get; set; }

        public List<ISsml> Elements { get; set; } = new List<ISsml>();

        public AmazonDomain(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public AmazonDomain(string name, params ISsml[] elements)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Elements = elements.ToList();
        }

        public XNode ToXml()
        {
            return new XElement(Namespaces.TempAmazon + "domain", new XAttribute("name", Name), Elements.Select(e => e.ToXml()));
        }
    }
}