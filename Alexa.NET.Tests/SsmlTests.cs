using System;
using Xunit;
using Alexa.NET.Response.Ssml;
using System.Xml.Linq;

namespace Alexa.NET.Tests
{
    public class SsmlTests
    {
        [Fact]
        public void Ssml_Error_With_No_Text()
        {
            var ssml = new Speech();

            Assert.Throws<InvalidOperationException>(() => ssml.ToXml());
        }

        [Fact]
        public void Ssml_Generates_Speak_And_Elements()
        {
            const string expected = "<speak>hello</speak>";
            var ssml = new Speech();

            ssml.Elements.Add(new PlainText("hello"));
            var actual = ssml.ToXml();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Ssml_PlainText_Generates_Text()
        {
            const string expected = "Hello World";

            var actual = new PlainText(expected).ToXml();

            Assert.Equal(expected, actual.ToString());
        }

        [Fact]
        public void Ssml_Sentence_With_Text_Generates_s()
        {
            const string expected = "<s>Hello World</s>";

            var actual = new Sentence("Hello World").ToXml();

            Assert.Equal(expected, actual.ToString());
        }

        [Fact]
        public void Ssml_Paragraph_Generates_p()
        {
            const string expected = "<p>Hello World</p>";

            var paragraph = new Paragraph();
            paragraph.Elements.Add(new PlainText("Hello World"));
            var actual = paragraph.ToXml();

            Assert.Equal(expected, actual.ToString());
        }

        [Fact]
        public void Ssml_Break_Generates_Break()
        {
            const string expected = "<break />";

            var actual = new Break().ToXml();

            Assert.Equal(expected, actual.ToString());
        }

        [Fact]
        public void Ssml_Break_Generates_Time_Attribute()
        {
            const string expected = @"<break time=""3s"" />";

            var actual = new Break{Time="3s"}.ToXml();

            Assert.Equal(expected,actual.ToString());
        }

        [Fact]
        public void Ssml_Break_Generates_Strength()
        {
            const string expected = @"<break strength=""x-weak"" />";

            var actual = new Break {Strength = BreakStrength.ExtraWeak}.ToXml();

            Assert.Equal(expected,actual.ToString());
        }
    }
}
