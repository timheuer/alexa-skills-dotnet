using System;
using System.Collections.Generic;
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

            var actual = new Break { Time = "3s" };

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Break_Generates_Strength()
        {
            const string expected = @"<break strength=""x-weak"" />";

            var actual = new Break { Strength = BreakStrength.ExtraWeak };

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Sayas_Generates_Sayas()
        {
            const string expected = @"<say-as interpret-as=""spell-out"">Hello World</say-as>";

            var actual = new SayAs("Hello World", InterpretAs.SpellOut);

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Sayas_Generates_Format()
        {
            const string expected = @"<say-as interpret-as=""spell-out"" format=""ymd"">Hello World</say-as>";

            var actual = new SayAs("Hello World", InterpretAs.SpellOut) { Format = "ymd" };

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

        [Fact]
        public void Ssml_Emphasis_generates_emphasis()
        {
            const string expected = @"<emphasis level=""strong"">Hello World</emphasis>";

            var actual = new Emphasis("Hello World");
            actual.Level = EmphasisLevel.Strong;

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Phoneme_generates_phoneme()
        {
            const string expected = @"<phoneme alphabet=""ipa"" ph=""pɪˈkɑːn"">pecan</phoneme>";

            var actual = new Phoneme("pecan", PhonemeAlphabet.International, "pɪˈkɑːn");

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Audio_generate_audio()
        {
            const string expected = @"<audio src=""http://example.com/example.mp3"">Hello World</audio>";

            var actual = new Audio("http://example.com/example.mp3");
            actual.Elements.Add(new PlainText("Hello World"));

            CompareXml(expected, actual);
        }

        [Fact]
        public void Ssml_Amazon_Effect_generate_amazon_effect()
        {
            const string expected = @"<speak><amazon:effect name=""whispered"">Hello World</amazon:effect></speak>";

            var effect = new AmazonEffect("Hello World");

            //Can't use Comparexml because this tag has meant a change to the speech element ToXml method
            var xmlHost = new Speech();
            xmlHost.Elements.Add(effect);
            var actual = xmlHost.ToXml();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TerseSsml_Produces_Identical_Xml()
        {
            const string speech1 = "Welcome to";
            const string speech2 = "the most awesome game ever";
            const string speech3 = "what do you want to do?";

            var expected = new Speech
            {
                Elements = new List<ISsml>
                {
                    new Paragraph
                    {
                        Elements = new List<IParagraphSsml>
                        {
                            new PlainText(speech1),
                            new Prosody
                            {
                                Rate = ProsodyRate.Fast,
                                Elements = new List<ISsml> {new Sentence(speech2)}
                            },
                            new Sentence(speech3)
                        }
                    }
                }
            };

            var actual = new Speech(
                new Paragraph(
                    new PlainText(speech1),
                    new Prosody(new Sentence(speech2)) { Rate = ProsodyRate.Fast },
                    new Sentence(speech3)
            ));

            Assert.Equal(expected.ToXml(), actual.ToXml());
        }

        [Fact]
        public void Ssml_VoiceAndLang_GenerateCorrectly()
        {
            var expected = "<voice name=\"Celine\"><lang xml:lang=\"fr-FR\">Je ne parle pas francais</lang></voice>";
            var speech =
                new Voice("Celine",
                        new Lang("fr-FR", new PlainText("Je ne parle pas francais"))
            );
            CompareXml(expected, speech);
        }

        [Fact]
        public void Ssml_Alexa_Name_generate_alexa_name()
        {
            const string expected = "<speak><alexa:name type=\"first\" personId=\"amzn1.ask.person.ABCDEF\" /></speak>";

            var alexaName = new AlexaName("amzn1.ask.person.ABCDEF");

            var xmlHost = new Speech();
            xmlHost.Elements.Add(alexaName);
            var actual = xmlHost.ToXml();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Ssml_AmazonDomain_generate_domain()
        {
            const string expected = @"<speak><amazon:domain name=""news"">A miniature manuscript</amazon:domain></speak>";

            var xmlHost = new Speech();
            var actual = new AmazonDomain(DomainName.News);
            actual.Elements.Add(new PlainText("A miniature manuscript"));
            xmlHost.Elements.Add(actual);
            Assert.Equal(expected, xmlHost.ToXml());
        }

        [Fact]
        public void Ssml_AmazonEmotion_generate_emotion()
        {
            const string expected = @"<speak><amazon:emotion name=""excited"" intensity=""medium"">Christina wins this round!</amazon:emotion></speak>";

            var xmlHost = new Speech();
            var actual = new AmazonEmotion(EmotionName.Excited, EmotionIntensity.Medium);
            actual.Elements.Add(new PlainText("Christina wins this round!"));
            xmlHost.Elements.Add(actual);
            Assert.Equal(expected,xmlHost.ToXml());
        }

        private void CompareXml(string expected, ISsml ssml)
        {
            var actual = ssml.ToXml().ToString(SaveOptions.DisableFormatting);
            Assert.Equal(expected, actual);
        }
    }
}
