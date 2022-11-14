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
            List<XAttribute> attributes = new();
            if (!string.IsNullOrWhiteSpace(Time))
            {
                attributes.Add(new("time", Time));
            }

            if (!string.IsNullOrWhiteSpace(Strength))
            {
                attributes.Add(new("strength",Strength));
            }
            return new XElement("break", attributes);
        }
    }
}
