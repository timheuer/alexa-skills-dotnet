using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;
using Xunit;

namespace Alexa.NET.Tests
{
    public class RepromptTests
    {
        [Fact]
        public void Create_Reprompt_with_implicit_conversion_string()
        {
            Reprompt reprompt = "test value";

            var output = reprompt.OutputSpeech as PlainTextOutputSpeech;

            Assert.NotNull(output);
            Assert.Equal("test value", ((PlainTextOutputSpeech)reprompt.OutputSpeech).Text);
        }

        [Fact]
        public void Create_Reprompt_with_implicit_conversion_SsmlSpeech()
        {
            Reprompt reprompt = new Speech(new SayAs("12345", "digits"));

            var output = reprompt.OutputSpeech as SsmlOutputSpeech;

            Assert.NotNull(output);
            Assert.Equal("<speak><say-as interpret-as=\"digits\">12345</say-as></speak>", output.Ssml);
        }
    }
}