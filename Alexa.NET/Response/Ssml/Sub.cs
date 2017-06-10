using System;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
    public class Sub:ICommonSsml
    {
        public Sub(string text, string alias)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
				throw new ArgumentNullException(nameof(text), "Text value required for Sub in Ssml");
            }

            if(string.IsNullOrWhiteSpace(alias))
            {
				throw new ArgumentNullException(nameof(alias), "Alias value required for Sub in Ssml");
            }

            Text = text;
            Alias = alias;
        }

        public string Text { get; set; }
        public string Alias { get; set; }

        public XNode ToXml()
        {
            return new XElement("sub",
                                new XAttribute("alias", Alias),
                                new XText(Text));
        }
    }
}
