using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
    public class Break : ICommonSsml
    {
        public string Time { get; set; }
        public string Strength { get; set; }

        public XNode ToXml()
        {
            List<XAttribute> attributes = new List<XAttribute>();
            if (!string.IsNullOrWhiteSpace(Time))
            {
                attributes.Add(new XAttribute("time", Time));
            }

            if (!string.IsNullOrWhiteSpace(Strength))
            {
                attributes.Add(new XAttribute("strength",Strength));
            }
            return new XElement("break", attributes);
        }
    }
}
