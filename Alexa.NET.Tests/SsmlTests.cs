﻿using System;
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

            var actual = new PlainText(expected);

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Sentence_With_Text_Generates_s()
        {
            const string expected = "<s>Hello World</s>";

            var actual = new Sentence("Hello World");

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Paragraph_Generates_p()
        {
            const string expected = "<p>Hello World</p>";

            var actual = new Paragraph();
            actual.Elements.Add(new PlainText("Hello World"));

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Break_Generates_Break()
        {
            const string expected = "<break />";

            var actual = new Break();

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Break_Generates_Time_Attribute()
        {
            const string expected = @"<break time=""3s"" />";

            var actual = new Break{Time="3s"};

            CompareXml(expected,actual);
        }

        [Fact]
        public void Ssml_Break_Generates_Strength()
        {
            const string expected = @"<break strength=""x-weak"" />";

            var actual = new Break {Strength = BreakStrength.ExtraWeak};

            CompareXml(expected,actual);
        }

        [Fact]
        public void Ssml_Sayas_Generates_Sayas()
        {
            const string expected = @"<say-as interpret-as=""spell-out"">Hello World</say-as>";

            var actual = new SayAs("Hello World",InterpretAs.SpellOut);

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Sayas_Generates_Format()
        {
			const string expected = @"<say-as interpret-as=""spell-out"" format=""ymd"">Hello World</say-as>";

            var actual = new SayAs("Hello World", InterpretAs.SpellOut){Format="ymd"};

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Word_Generates_w()
        {
            const string expected = @"<w role=""amazon:VB"">world</w>";

            var actual = new Word("world", WordRole.Verb);

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Sub_generates_sub()
        {
            const string expected = @"<sub alias=""magnesium"">Mg</sub>";

            var actual = new Sub("Mg", "magnesium");

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Prosody_generates_prosody()
        {
            const string expected = @"<prosody rate=""150%"" pitch=""x-low"" volume=""-5dB"">Hello World</prosody>";

            var actual = new Prosody
            {
                Rate = ProsodyRate.Percent(150),
                Pitch = ProsodyPitch.ExtraLow,
                Volume = ProsodyVolume.Decibel(-5)
            };

            actual.Elements.Add(new PlainText("Hello World"));

            CompareXml(expected, actual);
        }

      
        private void CompareXml(string expected, ISsml ssml)
        {
            var actual = ssml.ToXml().ToString(SaveOptions.DisableFormatting);
            Assert.Equal(expected,actual);
        }
    }
}
