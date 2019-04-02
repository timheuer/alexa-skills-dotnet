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
        [Fact]
        public void Create_Valid_DialogDelegateDirective()
        {
            var actual = new DialogDelegate { UpdatedIntent = GetUpdatedIntent() };

            Assert.True(Utility.CompareJson(actual, "DialogDelegate.json"));
        }

        [Fact]
        public void Create_Valid_DialogElicitSlotDirective()
        {
            var actual = new DialogElicitSlot("ZodiacSign") { UpdatedIntent = GetUpdatedIntent() };

            Assert.True(Utility.CompareJson(actual, "DialogElicitSlot.json"));
        }

        [Fact]
        public void Create_Valid_DialogConfirmSlotDirective()
        {
            var actual = new DialogConfirmSlot("Date") { UpdatedIntent = GetUpdatedIntent() };

            Assert.True(Utility.CompareJson(actual, "DialogConfirmSlot.json"));
        }

        [Fact]
        public void Create_Valid_DialogConfirmIntentDirective()
        {
            var actual = new DialogConfirmIntent { UpdatedIntent = GetUpdatedIntent() };
            actual.UpdatedIntent.Slots["ZodiacSign"].ConfirmationStatus = ConfirmationStatus.Confirmed;

            Assert.True(Utility.CompareJson(actual, "DialogConfirmIntent.json"));
        }

        [Fact]
        public void Create_Valid_DialogDynamicEntityDirective()
        {
            var actual = new DialogUpdateDynamicEntities { UpdateBehavior = UpdateBehavior.Replace };
            var airportSlotType = new SlotType
            {
                Name = "AirportSlotType",
                Values = new[]
                {
                    new SlotTypeValue
                    {
                        Id = "BOS",
                        Name = new SlotTypeValueName
                        {
                            Value = "Logan International Airport",
                            Synonyms = new[] {"Boston Logan"}
                        }
                    },
                    new SlotTypeValue
                    {
                        Id = "LGA",
                        Name = new SlotTypeValueName
                        {
                            Value = "LaGuardia Airport",
                            Synonyms = new[] {"New York"}
                        }
                    }
                }
            };
            actual.Types.Add(airportSlotType);
            Assert.True(Utility.CompareJson(actual, "DialogDynamicEntity.json"));
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
    }
}