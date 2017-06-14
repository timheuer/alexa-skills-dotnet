using System;
using System.Xml.Linq;

namespace Alexa.NET.Response.Ssml
{
    public class Phoneme : ICommonSsml
    {
        public string Text { get; set; }
        public string Alphabet { get; set; }
        public string Pronounciation { get; set; }

        public Phoneme(string text, string alphabet, string pronounciation)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(nameof(text), "Text value required for Phoneme in Ssml");
            }

            if (string.IsNullOrWhiteSpace(alphabet))
            {
                throw new ArgumentNullException(nameof(alphabet), "Alphabet value required for Phoneme in Ssml");
            }

            if (string.IsNullOrWhiteSpace(pronounciation))
            {
                throw new ArgumentNullException(nameof(pronounciation), "Pronounciation value required for Phoneme in Ssml");
            }

            Text = text;
            Alphabet = alphabet;
            Pronounciation = pronounciation;
        }

        public XNode ToXml()
        {
            return new XElement("phoneme",
                                new XAttribute("alphabet", Alphabet),
                                new XAttribute("ph", Pronounciation),
                                new XText(Text));
        }
    }
}
