using System;
using Xunit;
using Alexa.NET.Request;

namespace Alexa.NET.Tests
{
    public class IntentTests
    {
		//Multiple asserts in these tests only because they combine to test a single output state
		//https://developer.amazon.com/public/solutions/alexa/alexa-skills-kit/docs/understanding-the-structure-of-the-built-in-intent-library#property-values-passed-as-slot-values

		[Fact]
        public void Intent_action_set_string()
        {
            var expected = "CancelIntent";
            IntentSignature name = expected;

            Assert.Equal(expected,name.Action);
        }

        [Fact]
        public void Namespace_and_action_set_string()
        {
            IntentSignature name = "AMAZON.CancelIntent";

            Assert.Equal("AMAZON", name.Namespace);
            Assert.Equal("CancelIntent", name.Action);
        }

        [Fact]
        public void Complex_intent_single_property()
        {
            const string complexIntentSingleProperty = "AMAZON.SearchAction<object@WeatherForecast>";
            IntentSignature name = complexIntentSingleProperty;

			Assert.Equal("AMAZON", name.Namespace);
			Assert.Equal("SearchAction", name.Action);
            Assert.Equal(1,name.Properties.Count);

            Assert.True(name.Properties.ContainsKey("object"));
            Assert.Equal("WeatherForecast", name.Properties["object"].Entity);
        }

        [Fact]
        public void Complex_intent_two_properties()
        {
            const string complexIntentTwoProperties = "AMAZON.AddAction<object@Book,targetCollection@ReadingList>";

			IntentSignature name = complexIntentTwoProperties;

			Assert.Equal("AMAZON", name.Namespace);
			Assert.Equal("AddAction", name.Action);
			Assert.Equal(2, name.Properties.Count);

			Assert.True(name.Properties.ContainsKey("object"));
			Assert.Equal("Book", name.Properties["object"].Entity);

			Assert.True(name.Properties.ContainsKey("targetCollection"));
            Assert.Equal("ReadingList", name.Properties["targetCollection"].Entity);
        }

        [Fact]
        public void Complex_with_entity_property()
        {
            const string complexIntentWithEntityProperty = "AMAZON.SearchAction<object@WeatherForecast[weatherCondition]>";

			IntentSignature name = complexIntentWithEntityProperty;

			Assert.Equal("AMAZON", name.Namespace);
			Assert.Equal("SearchAction", name.Action);
			Assert.Equal(1, name.Properties.Count);

			Assert.True(name.Properties.ContainsKey("object"));
			Assert.Equal("WeatherForecast", name.Properties["object"].Entity);
            Assert.Equal("weatherCondition", name.Properties["object"].Property);
        }
    }
}
