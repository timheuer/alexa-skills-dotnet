using Xunit;
using System.IO;
using Newtonsoft.Json.Linq;
using Alexa.NET.Response;

namespace Alexa.NET.Tests
{
    public class CardTests
    {
        private const string ExamplesPath = "Examples";

        [Fact]
        public void Creates_Valid_SimpleCard()
        {
            var actual = new SimpleCard { Title = "Example Title", Content = "Example Content" };

            Assert.True(CompareJson(actual, "SimpleCard.json"));
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
