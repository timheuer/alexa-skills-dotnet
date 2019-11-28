using System;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
    public class AlexaName : ICommonSsml
    {
        public AlexaName() { }

        public AlexaName(string personId)
        {
            if (string.IsNullOrWhiteSpace(personId))
            {
                throw new ArgumentNullException(nameof(personId), "PersonId value required for AlexaName in Ssml");
            }
            PersonId = personId;
        }

        public string PersonId { get; set; }

        public XNode ToXml()
        {
            return new XElement(Namespaces.TempAlexa + "name", 
                new XAttribute("type", "first"), new XAttribute("personId",PersonId));
        }
    }
}