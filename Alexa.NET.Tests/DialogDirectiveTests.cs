using System;
using Xunit;
using System.IO;
using Alexa.NET.Request;
using Newtonsoft.Json.Linq;
using Alexa.NET.Response.Directive;

namespace Alexa.NET.Tests
{
    public class DialogDirectiveTests
    {
        private const string ExamplesPath = "Examples";

        [Fact]
        public void Create_Valid_DialogDelegateDirective()
        {
            var actual = new DialogDelegate{UpdatedIntent=GetUpdatedIntent()};

            Assert.True(CompareJson(actual, "DialogDelegate.json"));
        }

        [Fact]
        public void Create_Valid_DialogElicitSlotDirective()
        {
            var actual = new DialogElicitSlot("ZodiacSign") { UpdatedIntent = GetUpdatedIntent() };

			Assert.True(CompareJson(actual, "DialogElicitSlot.json"));
        }

		[Fact]
		public void Create_Valid_DialogConfirmSlotDirective()
		{
			var actual = new DialogConfirmSlot("Date") { UpdatedIntent = GetUpdatedIntent() };

			Assert.True(CompareJson(actual, "DialogConfirmSlot.json"));
		}

        [Fact]
        public void Create_Valid_DialogConfirmIntentDirective()
        {
            var actual = new DialogConfirmIntent { UpdatedIntent = GetUpdatedIntent() };
            actual.UpdatedIntent.Slots["ZodiacSign"].ConfirmationStatus = ConfirmationStatus.Confirmed;

            Assert.True(CompareJson(actual, "DialogConfirmIntent.json"));
        }

        private Intent GetUpdatedIntent()
        {
			return new Intent
			{
				Name = "GetZodiacHoroscopeIntent",
				ConfirmationStatus = ConfirmationStatus.None,
				Slots = new System.Collections.Generic.Dictionary<string, Slot>{
					{"ZodiacSign",new Slot{Name="ZodiacSign",Value="virgo"}},
						{"Date",new Slot{Name="Date",Value="2015-11-25",ConfirmationStatus=ConfirmationStatus.Confirmed}}
				}
			};
        }

        private bool CompareJson(object actual, string expectedFile)
        {

            var actualJObject = JObject.FromObject(actual);
            var expected = File.ReadAllText(Path.Combine(ExamplesPath, expectedFile));
            var expectedJObject = JObject.Parse(expected);
            Console.WriteLine(actualJObject);
            return JToken.DeepEquals(expectedJObject, actualJObject);
        }
    }
}