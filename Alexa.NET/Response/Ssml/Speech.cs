using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
    public class Speech
    {

        public List<ISsml> Elements { get; set; } = new List<ISsml>();


        public string ToXml()
        {
            if (Elements.Count == 0)
            {
                throw new InvalidOperationException("No text available");
            }

            XElement root = new XElement("speak", Elements.Select(e => e.ToXml()));
            string wellFormedXml = root.ToString(SaveOptions.DisableFormatting);
            return wellFormedXml.Replace(" xmlns:amazon=\"http://alexa.amazon.com\"", string.Empty);
        }
    }
}
