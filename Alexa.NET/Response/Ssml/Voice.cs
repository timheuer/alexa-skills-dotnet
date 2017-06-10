using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
    public class Voice:ICommonSsml
    {
		public List<ISsml> Elements { get; set; } = new List<ISsml>();

		public XNode ToXml()
		{
			return new XElement("voice", Elements.Select(e => e.ToXml()));
		}
    }
}
