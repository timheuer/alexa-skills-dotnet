using System;
using Alexa.NET.ConnectionTasks;
using Alexa.NET.ConnectionTasks.Inputs;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
using Alexa.NET.Tests.Examples;
using Xunit;

namespace Alexa.NET.Tests
{
    public class SkillConnectionTests
    {
        [Fact]
        public void StartConnectionDirectiveSerializesCorrectly()
        {
            var task = new PrintPdfV1
            {
                Title = "title",
                Description = "description",
                Url = "http://www.example.com/flywheel.pdf",
                Context = new ConnectionTaskContext { ProviderId = "your-provider-skill-id" }
            };
            Utility.CompareJson(task.ToConnectionDirective("none"), "PrintPDFConnection.json");
        }

        [Fact]
        public void StartConnectionDirectiveDeserializesCorrectly()
        {
            var raw = Utility.ExampleFileContent<IDirective>("PrintPDFConnection.json");
            Assert.NotNull(raw);

            var directive = Assert.IsType<StartConnectionDirective>(raw);
            Assert.Equal("none", directive.Token);
            Assert.Equal(PrintPdfV1.AssociatedUri, directive.Uri);

            Assert.IsType<PrintPdfV1>(directive.Input);
        }

        [Fact]
        public void SessionResumedSerializesProperly()
        {
            var task = new SessionResumedRequest
            {
                RequestId = "string",
                Timestamp = new DateTime(2019, 07, 03),
                Locale = "en-GB",
                Type = "SessionResumedRequest",
                OriginIpAddress = "string",
                Cause = new SessionResumedRequestCause
                {
                    Type = "ConnectionCompleted",
                    Token = "1234",
                    Status = new ConnectionStatus(200, "OK")
                }
            };
            Utility.CompareJson(task, "SessionResumedRequest.json");
        }

        [Fact]
        public void SessionResumedDeserializesProperly()
        {
            var result = Utility.ExampleFileContent<Request.Type.Request>("SessionResumedRequest.json");
            var request = Assert.IsType<SessionResumedRequest>(result);

            Assert.Equal("1234", request.Cause.Token);
            Assert.Equal(200, request.Cause.Status.Code);
            Assert.Equal("OK", request.Cause.Status.Message);
        }

        [Fact]
        public void LaunchRequestWithTaskDeserializesCorrectly()
        {
            var result = Utility.ExampleFileContent<LaunchRequest>("LaunchRequestWithTask.json");
            Assert.NotNull(result.Task);
            Assert.Equal("AMAZON.PrintPDF", result.Task.Name);
            Assert.Equal("1", result.Task.Version);
            Assert.IsType<PrintPdfV1>(result.Task.Input);
        }

        [Fact]
        public void LaunchRequestWithCustomTaskDeserializesCorrectly()
        {
            ExampleTaskConverter.AddToConnectionTaskConverters();
            var result = Utility.ExampleFileContent<LaunchRequest>("LaunchRequestWithCustomTask.json");
            Assert.NotNull(result.Task);
            Assert.Equal("Custom.ExampleTask", result.Task.Name);
            Assert.Equal("1", result.Task.Version);
            Assert.IsType<ExampleTask>(result.Task.Input);
            Assert.Equal(((ExampleTask)result.Task.Input).RandomParameter, "parameterValue");
        }

        [Fact]
        public void TestCompleteTaskDirective()
        {
            var directive = new CompleteTaskDirective(200, "return as desired");
            Assert.True(Utility.CompareJson(directive, "CompleteTaskDirective.json"));
        }

        [Fact]
        public void PrintImageConnectionComparison()
        {
            var directive = new PrintImageV1
            {
                Title = "Flywheel Document",
                Description = "Flywheel",
                ImageV1Type = PrintImageV1Type.JPEG,
                Url = "http://www.example.com/flywheel.jpeg"
            }.ToConnectionDirective();
            Assert.Equal(PrintImageV1.AssociatedUri, directive.Uri);
            Assert.True(Utility.CompareJson(directive, "PrintImageConnection.json"));
            Assert.IsType<PrintImageV1>(Utility.ExampleFileContent<StartConnectionDirective>("PrintImageConnection.json").Input);
        }

        [Fact]
        public void PrintPDFConnectionComparison()
        {
            var directive = new PrintPdfV1
            {
                Title = "title",
                Description = "description",
                Url = "http://www.example.com/flywheel.pdf",
                Context = new ConnectionTaskContext { ProviderId = "your-provider-skill-id" }
            }.ToConnectionDirective("none");
            Assert.Equal(PrintPdfV1.AssociatedUri, directive.Uri);
            Assert.True(Utility.CompareJson(directive, "PrintPDFConnection.json"));
            Assert.IsType<PrintPdfV1>(Utility.ExampleFileContent<StartConnectionDirective>("PrintPDFConnection.json").Input);
        }

        [Fact]
        public void PrintWebPageConnectionComparison()
        {
            var directive = new PrintWebPageV1
            {
                Title = "title",
                Description = "description",
                Url = "http://www.example.com/flywheel.html"
            }.ToConnectionDirective();
            Assert.Equal(PrintWebPageV1.AssociatedUri, directive.Uri);
            Assert.True(Utility.CompareJson(directive, "PrintWebPageConnection.json"));
            Assert.IsType<PrintPdfV1>(Utility.ExampleFileContent<StartConnectionDirective>("PrintPDFConnection.json").Input);
        }

        [Fact]
        public void ScheduleTaxiReservationConnectionComparison()
        {
            var directive = new ScheduleTaxiReservation
            {
                PartySize = 4,
                PickupLocation = new PostalAddress
                {
                    StreetAddress = "415 106th Ave NE",
                    Locality = "Bellevue",
                    Region = "WA",
                    PostalCode = "98004",
                    Country = "US"
                },
                DropoffLocation = new PostalAddress
                {
                    StreetAddress = "2031 6th Ave.",
                    Locality = "Seattle",
                    Region = "WA",
                    PostalCode = "98121",
                    Country = "US"
                }
            }.ToConnectionDirective();
            Assert.Equal(ScheduleTaxiReservation.AssociatedUri, directive.Uri);
            Assert.True(Utility.CompareJson(directive, "ScheduleTaxiReservation.json"));
            Assert.IsType<ScheduleTaxiReservation>(Utility.ExampleFileContent<StartConnectionDirective>("ScheduleTaxiReservation.json").Input);
        }

        [Fact]
        public void ScheduleFoodReservationConnectionComparison()
        {
            var directive = new ScheduleFoodEstablishmentReservation
            {
                PartySize = 2,
                StartTime = new DateTime(2018,04,08,01,15,46),
                Restaurant = new Restaurant
                {
                    Name = "Amazon Day 1 Restaurant",
                    Location = new PostalAddress
                    {
                        StreetAddress = "2121 7th Avenue",
                        Locality = "Seattle",
                        Region = "WA",
                        PostalCode = "98121",
                        Country = "US"
                    }
                }
            }.ToConnectionDirective();
            directive.OnComplete = OnCompleteAction.ResumeSession;
            Assert.Equal(ScheduleFoodEstablishmentReservation.AssociatedUri, directive.Uri);
            Assert.True(Utility.CompareJson(directive, "ScheduleFoodEstablishmentReservation.json"));
            Assert.IsType<ScheduleFoodEstablishmentReservation>(Utility.ExampleFileContent<StartConnectionDirective>("ScheduleFoodEstablishmentReservation.json").Input);
        }

        [Fact]
        public void PinConfirmationSerializesCorrectly()
        {
            var task = new PinConfirmation();
            Assert.True(Utility.CompareJson(task.ToConnectionDirective("example-token"), "PinConfirmation.json"));
        }

        [Fact]
        public void PinConfirmationDeserializesCorrectly()
        {
            PinConfirmationConverter.AddToConnectionTaskConverters();
            var task = Utility.ExampleFileContent<StartConnectionDirective>("PinConfirmation.json");
            Assert.IsType<PinConfirmation>(task.Input);
        }

        [Fact]
        public void PinResultDeserializesCorrectly()
        {
            var request = Utility.ExampleFileContent<SessionResumedRequest>("PinConfirmationSessionResumed.json");
            var result = PinConfirmationConverter.ResultFromSessionResumed(request);
            Assert.Equal(PinConfirmationStatus.NotAchieved,result.Status);
            Assert.Equal(PinConfirmationReason.VerificationMethodNotSetup,result.Reason);
        }

        [Fact]
        public void SendToPhoneSerialization()
        {
            SendToPhoneConverter.AddToConnectionTaskConverters();
            var task = Utility.ExampleFileContent<StartConnectionDirective>("SendToPhoneUniversal.json");
            Assert.IsType<SendToPhone>(task.Input);
            Assert.True(Utility.CompareJson(task,"SendToPhoneUniversal.json"));
        }

        [Fact]
        public void SendToPhoneUniversal()
        {
            SendToPhoneConverter.AddToConnectionTaskConverters();
            var task = Utility.ExampleFileContent<StartConnectionDirective>("SendToPhoneUniversal.json");
            var stp = Assert.IsType<SendToPhone>(task.Input);
            var links = stp.Links;
            var iosPrimary = Assert.IsType<STPUniversalLink>(links.IOSAppStore.Primary);
            var googlePrimary = Assert.IsType<STPUniversalLink>(links.GooglePlayStore.Primary);
            Assert.Equal("id123456789",iosPrimary.AppIdentifier);
            Assert.Equal("com.cityguide.app", googlePrimary.AppIdentifier);
            Assert.Equal("https://www.cityguide.com/search/search_terms=coffee", googlePrimary.Url);
            Assert.Equal("https://www.cityguide.com/search/search_terms=coffee", iosPrimary.Url);
            Assert.Equal(STPPromptBehavior.Speak,stp.Prompt.DirectLaunchDefaultPromptBehavior);
        }

        [Fact]
        public void SendToPhoneWebsiteUrl()
        {
            SendToPhoneConverter.AddToConnectionTaskConverters();
            var task = Utility.ExampleFileContent<StartConnectionDirective>("SendToPhoneWebsite.json");
            var stp = Assert.IsType<SendToPhone>(task.Input);
            var iosFallback = Assert.IsType<STPWebsiteLink>(stp.Links.IOSAppStore.Fallback);
            Assert.Equal("https://www.cityguide.com/search/search_terms=coffee",iosFallback.Url);
        }

        [Fact]
        public void SendToPhoneAndroidCustomIntent()
        {
            SendToPhoneConverter.AddToConnectionTaskConverters();
            var task = Utility.ExampleFileContent<StartConnectionDirective>("SendToPhoneAndroidCustom.json");
            var stp = Assert.IsType<SendToPhone>(task.Input);
            var googlePrimary = Assert.IsType<STPAndroidCustomIntentLink>(stp.Links.GooglePlayStore.Primary);
            Assert.Equal("com.someapp", googlePrimary.AppIdentifier);
            Assert.Equal("intent:#Intent;package=com.someapp;action=com.example.myapp.MY_ACTION;i.some_int=100;S.some_str=hello;end", googlePrimary.IntentSchemeUri);
        }

        [Fact]
        public void SendToPhoneCustomScheme()
        {
            SendToPhoneConverter.AddToConnectionTaskConverters();
            var task = Utility.ExampleFileContent<StartConnectionDirective>("SendToPhoneCustomScheme.json");
            var stp = Assert.IsType<SendToPhone>(task.Input);
            var googlePrimary = Assert.IsType<STPCustomSchemeLink>(stp.Links.GooglePlayStore.Primary);
            Assert.Equal("id123456789", googlePrimary.AppIdentifier);
            Assert.Equal("twitter://feeds/", googlePrimary.Uri);
        }

        [Fact]
        public void SendToPhoneCommonScheme()
        {
            SendToPhoneConverter.AddToConnectionTaskConverters();
            var task = Utility.ExampleFileContent<StartConnectionDirective>("SendToPhoneCommonScheme.json");
            var stp = Assert.IsType<SendToPhone>(task.Input);
            var googlePrimary = Assert.IsType<STPCommonSchemeLink>(stp.Links.GooglePlayStore.Primary);
            Assert.Equal("TEL", googlePrimary.Scheme);
            Assert.Equal("tel:8001234567", googlePrimary.Uri);
        }

        [Fact]
        public void SendToPhoneAndroidPackage()
        {
            SendToPhoneConverter.AddToConnectionTaskConverters();
            var task = Utility.ExampleFileContent<StartConnectionDirective>("SendToPhoneAndroidPackage.json");
            var stp = Assert.IsType<SendToPhone>(task.Input);
            var googlePrimary = Assert.IsType<STPAndroidPackageLink>(stp.Links.GooglePlayStore.Primary);
            Assert.Equal("com.someapp", googlePrimary.PackageIdentifier);
        }

        [Fact]
        public void SendToPhoneAndroidCommonIntent()
        {
            SendToPhoneConverter.AddToConnectionTaskConverters();
            var task = Utility.ExampleFileContent<StartConnectionDirective>("SendToPhoneAndroidCommonIntent.json");
            var stp = Assert.IsType<SendToPhone>(task.Input);
            var googlePrimary = Assert.IsType<STPAndroidCommonIntentLink>(stp.Links.GooglePlayStore.Primary);
            Assert.Equal("OPEN_SETTINGS", googlePrimary.IntentName);
            Assert.Equal("intent:#Intent;action=android.settings.WIFI_SETTINGS;end", googlePrimary.IntentSchemeUri);
        }

        [Fact]
        public void SendToPhoneResultDirectLaunchDeserializesCorrectly()
        {
            var request = Utility.ExampleFileContent<SessionResumedRequest>("SendToPhoneSessionResumedDirectLaunch.json");
            var result = SendToPhoneConverter.ResultFromSessionResumed(request);
            Assert.Equal(STPResultStatus.Success, result.DirectLaunch.Primary.Status);
            Assert.Equal(STPResultStatus.Failure, result.DirectLaunch.Fallback.Status);
            Assert.Null(result.DirectLaunch.Primary.ErrorCode);
            Assert.Equal("INVALID_STATE", result.DirectLaunch.Fallback.ErrorCode);
        }

        [Fact]
        public void SendToPhoneResultSendToDeviceDeserializesCorrectly()
        {
            var request = Utility.ExampleFileContent<SessionResumedRequest>("SendToPhoneSessionResumedSendToDevice.json");
            var result = SendToPhoneConverter.ResultFromSessionResumed(request);
            Assert.Equal(STPResultStatus.Success, result.SendToDevice.Status);
            Assert.Equal("ALL_ATTEMPTED_CARD_SENT", result.SendToDevice.ErrorCode);
        }

        [Fact]
        public void SendToPhoneResultFailsEarly()
        {
            var request = Utility.ExampleFileContent<SessionResumedRequest>("SendToPhoneSessionResumedFailsEarly.json");
            var result = SendToPhoneConverter.ResultFromSessionResumed(request);
            Assert.Null(result);
        }
    }
}
