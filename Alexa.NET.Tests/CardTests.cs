using Xunit;
using System.IO;
using Newtonsoft.Json.Linq;
using Alexa.NET.Response;

namespace Alexa.NET.Tests
{
    public class CardTests
    {
        private const string ExamplesPath = "Examples";
        private const string ExampleTitle = "Example Title";
        private const string ExampleBodyText = "Example Body Text";

        [Fact]
        public void Creates_Valid_SimpleCard()
        {
            var actual = new SimpleCard { Title = "Example Title", Content = "Example Content" };

            Assert.True(CompareJson(actual, "SimpleCard.json"));
        }

        [Fact]
        public void Creates_Valid_StandardCard()
        {
            var actual = new StandardCard{Title="Example Title",}
        }

        private bool CompareJson(object actual, string expectedFile)
        {
            var actualJObject = JObject.FromObject(actual);

            var expected = File.ReadAllText(Path.Combine(ExamplesPath, expectedFile));
            var expectedJObject = JObject.Parse(expected);

            return JToken.DeepEquals(expectedJObject, actualJObject);
        }
    }
}
