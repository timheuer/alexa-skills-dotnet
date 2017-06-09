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
            var actual = new SimpleCard { Title = ExampleTitle, Content = ExampleBodyText };

            Assert.True(CompareJson(actual, "SimpleCard.json"));
        }

        [Fact]
        public void Creates_Valid_StandardCard()
        {
            var cardImages = new CardImage { SmallImageUrl = "https://example.com/smallImage.png", LargeImageUrl = "https://example.com/largeImage.png" };
            var actual = new StandardCard{ Title = ExampleTitle, Content = ExampleBodyText,Image=cardImages };

            Assert.True(CompareJson(actual, "StandardCard.json"));
        }

        [Fact]
        public void Creates_Valid_AskForPermissionConsent()
        {
            var actual = new AskForPermissionsConsentCard();
            actual.Permissions.Add(RequestedPermission.ReadHouseholdList);

            Assert.True(CompareJson(actual, "AskForPermissionsConsent.json"));
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
