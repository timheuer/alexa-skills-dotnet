using System;
using System.Xml.Linq;
namespace Alexa.NET.Response.Ssml
{
    public interface ISsml
    {
        XNode ToXml();
    }
}
