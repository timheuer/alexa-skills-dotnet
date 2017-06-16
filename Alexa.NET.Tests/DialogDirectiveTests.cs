using System;
using Xunit;
using System.IO;
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
            var updatedIntent = new Intent
            {
                Name = "GetZodiacHoroscopeIntent",
                ConfirmationStatus = ConfirmationStatus.None,
                Slots = new System.Collections.Generic.Dictionary<string, Slot>{
                    {"ZodiacSign",new Slot{Name="ZodiacSign",Value="virgo"}},
                        {"Date",new Slot{Name="Date",Value="2015-11-25",ConfirmationStatus=ConfirmationStatus.Confirmed}}
                }
            };
            var actual = new DialogDelegate{UpdatedIntent=updatedIntent};


            Assert.True(CompareJson(actual, "DialogDelegate.json"));
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