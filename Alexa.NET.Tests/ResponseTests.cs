using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
using Alexa.NET.Response.Directive.Templates;
using Alexa.NET.Response.Directive.Templates.Types;
using Alexa.NET.Response.Ssml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace Alexa.NET.Tests
{
    public class ResponseTests
    {
        private const string ExamplesPath = @"Examples";

        [Fact]
        public void Should_create_same_json_response_as_example()
        {
            var skillResponse = new SkillResponse
            {
                Version = "1.0",
                SessionAttributes = new Dictionary<string, object>
                {
                    {
                        "supportedHoriscopePeriods", new
                        {
                            daily = true,
                            weekly = false,
                            monthly = false
                        }
                    }
                },
                Response = new ResponseBody
                {
                    OutputSpeech = new PlainTextOutputSpeech
                    {
                        Text =
                            "Today will provide you a new learning opportunity. Stick with it and the possibilities will be endless. Can I help you with anything else?"
                    },
                    Card = new SimpleCard
                    {
                        Title = "Horoscope",
                        Content =
                            "Today will provide you a new learning opportunity. Stick with it and the possibilities will be endless."
                    },
                    ShouldEndSession = false
                }
            };

            Assert.True(Utility.CompareJson(skillResponse, "Response.json"));
        }

        [Fact]
        public void Creates_VideoAppDirective()
        {
            var videoItem = new VideoItem("https://www.example.com/video/sample-video-1.mp4")
            {
                Metadata = new VideoItemMetadata
                {
                    Title = "Title for Sample Video",
                    Subtitle = "Secondary Title for Sample Video"
                }
            };
            var actual = new VideoAppDirective { VideoItem = videoItem };

            Assert.True(Utility.CompareJson(actual, "VideoAppDirectiveWithMetadata.json"));
        }

        [Fact]
        public void Create_VideoAppDirective_FromSource()
        {
            var actual = new VideoAppDirective("https://www.example.com/video/sample-video-1.mp4");
            Assert.True(Utility.CompareJson(actual, "VideoAppDirective.json"));
        }

        [Fact]
        public void Create_HintDirective()
        {
            var actual = new HintDirective { Hint = new Hint { Text = "sample text", Type = TextType.Plain } };
            var expected = JObject.Parse(@"{
            ""type"": ""Hint"",
            ""hint"": {
                ""type"": ""PlainText"",
                ""text"": ""sample text""
            }
        }");
            Assert.True(CompareJson(actual, expected));
        }

        [Fact]
        public void AudioPlayerGeneratesCorrectJson()
        {
            var directive = new AudioPlayerPlayDirective
            {
                PlayBehavior = PlayBehavior.Enqueue,
                AudioItem = new AudioItem
                {
                    Stream = new AudioItemStream
                    {
                        Url = "https://url-of-the-stream-to-play",
                        Token = "opaque token representing this stream",
                        ExpectedPreviousToken = "opaque token representing the previous stream"
                    }
                }
            };
            Assert.True(Utility.CompareJson(directive, "AudioPlayerWithoutMetadata.json"));
        }

        [Fact]
        public void AudioPlayerWithMetadataGeneratesCorrectJson()
        {
            var directive = new AudioPlayerPlayDirective
            {
                PlayBehavior = PlayBehavior.Enqueue,
                AudioItem = new AudioItem
                {
                    Stream = new AudioItemStream
                    {
                        Url = "https://url-of-the-stream-to-play",
                        Token = "opaque token representing this stream",
                        ExpectedPreviousToken = "opaque token representing the previous stream"
                    },
                    Metadata = new AudioItemMetadata
                    {
                        Title = "title of the track to display",
                        Subtitle = "subtitle of the track to display",
                        Art = new AudioItemSources
                        {
                            Sources = new[] { new AudioItemSource("https://url-of-the-album-art-image.png") }.ToList()
                        },
                        BackgroundImage = new AudioItemSources { Sources = new[] { new AudioItemSource("https://url-of-the-background-image.png") }.ToList() }
                    }
                }
            };
            Assert.True(Utility.CompareJson(directive, "AudioPlayerWithMetadata.json"));
        }

        [Fact]
        public void AudioPlayerWithMetadataDeserializesCorrectly()
        {
            var audioPlayer = Utility.ExampleFileContent<AudioPlayerPlayDirective>("AudioPlayerWithMetadata.json");
            Assert.Equal("title of the track to display", audioPlayer.AudioItem.Metadata.Title);
            Assert.Equal("subtitle of the track to display", audioPlayer.AudioItem.Metadata.Subtitle);
            Assert.Single(audioPlayer.AudioItem.Metadata.Art.Sources);
            Assert.Single(audioPlayer.AudioItem.Metadata.BackgroundImage.Sources);
            Assert.Equal("https://url-of-the-album-art-image.png", audioPlayer.AudioItem.Metadata.Art.Sources.First().Url);
            Assert.Equal("https://url-of-the-background-image.png", audioPlayer.AudioItem.Metadata.BackgroundImage.Sources.First().Url);
        }

        [Fact]
        public void AudioPlayerIgnoresMetadataWhenNull()
        {
            var audioPlayer = Utility.ExampleFileContent<AudioPlayerPlayDirective>("AudioPlayerWithoutMetadata.json");
            Assert.Null(audioPlayer.AudioItem.Metadata);
            Assert.Equal("https://url-of-the-stream-to-play", audioPlayer.AudioItem.Stream.Url);
        }

        [Fact]
        public void RepromptStringGeneratesPlainTextOutput()
        {
            var result = new Reprompt("text");
            Assert.IsType<PlainTextOutputSpeech>(result.OutputSpeech);
            var plainText = (PlainTextOutputSpeech)result.OutputSpeech;
            Assert.Equal("text", plainText.Text);
        }

        [Fact]
        public void RepromptSsmlGeneratesPlainTextOutput()
        {
            var speech = new Speech(new PlainText("text"));
            var result = new Reprompt(speech);
            Assert.IsType<SsmlOutputSpeech>(result.OutputSpeech);
            var ssmlText = (SsmlOutputSpeech)result.OutputSpeech;
            Assert.Equal(speech.ToXml(), ssmlText.Ssml);
        }

        [Fact]
        public void DeserializesExampleSimpleCardJson()
        {
            var deserialized = Utility.ExampleFileContent<ICard>("SimpleCard.json");

            Assert.Equal(typeof(SimpleCard), deserialized.GetType());

            var card = (SimpleCard)deserialized;

            Assert.Equal("Simple", card.Type);
            Assert.Equal("Example Title", card.Title);
            Assert.Equal("Example Body Text", card.Content);
        }

        [Fact]
        public void DeserializesExampleStandardCardJson()
        {
            var deserialized = Utility.ExampleFileContent<ICard>("StandardCard.json");

            Assert.Equal(typeof(StandardCard), deserialized.GetType());

            var card = (StandardCard)deserialized;

            Assert.Equal("Standard", card.Type);
            Assert.Equal("Example Title", card.Title);
            Assert.Equal("Example Body Text", card.Content);
            Assert.Equal("https://example.com/smallImage.png", card.Image.SmallImageUrl);
            Assert.Equal("https://example.com/largeImage.png", card.Image.LargeImageUrl);
        }

        [Fact]
        public void DeserializesExampleLinkAccountCardJson()
        {
            var deserialized = Utility.ExampleFileContent<ICard>("LinkAccountCard.json");

            Assert.Equal(typeof(LinkAccountCard), deserialized.GetType());

            var card = (LinkAccountCard)deserialized;

            Assert.Equal("LinkAccount", card.Type);
        }

        [Fact]
        public void DeserializesExampleAskForPermissionsConsentJson()
        {
            var deserialized = Utility.ExampleFileContent<ICard>("AskForPermissionsConsent.json");

            Assert.Equal(typeof(AskForPermissionsConsentCard), deserialized.GetType());

            var card = (AskForPermissionsConsentCard)deserialized;

            Assert.Equal("AskForPermissionsConsent", card.Type);
            Assert.Equal(2, card.Permissions.Count);
            Assert.NotEqual("read::alexa:household:list", card.Permissions.First());
            Assert.Equal("alexa::household:lists:read", card.Permissions.First());
            Assert.Equal("alexa::household:lists:write", card.Permissions.Last());
        }

        [Fact]
        public void SerializesPlainTextOutputSpeechToJson()
        {
            var plainTextOutputSpeech = new PlainTextOutputSpeech { Text = "text content" };
            Assert.True(Utility.CompareJson(plainTextOutputSpeech, "PlainTextOutputSpeech.json"));
        }

        [Fact]
        public void DeserializesExamplePlainTextOutputSpeechJson()
        {
            var deserialized = Utility.ExampleFileContent<IOutputSpeech>("PlainTextOutputSpeech.json");

            Assert.Equal(typeof(PlainTextOutputSpeech), deserialized.GetType());

            var outputSpeech = (PlainTextOutputSpeech)deserialized;

            Assert.Equal("PlainText", outputSpeech.Type);
            Assert.Equal("text content", outputSpeech.Text);
            Assert.Equal(null, outputSpeech.PlayBehavior);
        }

        [Fact]
        public void SerializesPlainTextOutputSpeechWithPlayBehaviorToJson()
        {
            var plainTextOutputSpeech = new PlainTextOutputSpeech { Text = "text content", PlayBehavior = PlayBehavior.ReplaceAll };
            Assert.True(Utility.CompareJson(plainTextOutputSpeech, "PlainTextOutputSpeechWithPlayBehavior.json"));
        }

        [Fact]
        public void DeserializesExamplePlainTextOutputSpeechWithPlayBehaviorJson()
        {
            var deserialized = Utility.ExampleFileContent<IOutputSpeech>("PlainTextOutputSpeechWithPlayBehavior.json");

            Assert.Equal(typeof(PlainTextOutputSpeech), deserialized.GetType());

            var outputSpeech = (PlainTextOutputSpeech)deserialized;

            Assert.Equal("PlainText", outputSpeech.Type);
            Assert.Equal("text content", outputSpeech.Text);
            Assert.Equal(PlayBehavior.ReplaceAll, outputSpeech.PlayBehavior);
        }

        [Fact]
        public void SerializesSsmlOutputSpeechToJson()
        {
            var ssmlOutputSpeech = new SsmlOutputSpeech { Ssml = "ssml content" };
            Assert.True(Utility.CompareJson(ssmlOutputSpeech, "SsmlOutputSpeech.json"));
        }

        [Fact]
        public void DeserializesExampleSsmlOutputSpeechJson()
        {
            var deserialized = Utility.ExampleFileContent<IOutputSpeech>("SsmlOutputSpeech.json");

            Assert.Equal(typeof(SsmlOutputSpeech), deserialized.GetType());

            var outputSpeech = (SsmlOutputSpeech)deserialized;

            Assert.Equal("SSML", outputSpeech.Type);
            Assert.Equal("ssml content", outputSpeech.Ssml);
            Assert.Equal(null, outputSpeech.PlayBehavior);
        }

        [Fact]
        public void SerializesSsmlOutputSpeechWithPlayBehaviorToJson()
        {
            var ssmlOutputSpeech = new SsmlOutputSpeech { Ssml = "ssml content", PlayBehavior = PlayBehavior.ReplaceEnqueued };
            Assert.True(Utility.CompareJson(ssmlOutputSpeech, "SsmlOutputSpeechWithPlayBehavior.json"));
        }

        [Fact]
        public void DeserializesExampleSsmlOutputSpeechWithPlayBehaviorJson()
        {
            var deserialized = Utility.ExampleFileContent<IOutputSpeech>("SsmlOutputSpeechWithPlayBehavior.json");

            Assert.Equal(typeof(SsmlOutputSpeech), deserialized.GetType());

            var outputSpeech = (SsmlOutputSpeech)deserialized;

            Assert.Equal("SSML", outputSpeech.Type);
            Assert.Equal("ssml content", outputSpeech.Ssml);
            Assert.Equal(PlayBehavior.ReplaceEnqueued, outputSpeech.PlayBehavior);
        }

        [Fact]
        public void DeserializesExampleAudioPlayerWithMetadataJson()
        {
            var deserialized = Utility.ExampleFileContent<IDirective>("AudioPlayerWithMetadata.json");

            Assert.Equal(typeof(AudioPlayerPlayDirective), deserialized.GetType());

            var directive = (AudioPlayerPlayDirective)deserialized;

            Assert.Equal("AudioPlayer.Play", directive.Type);
            Assert.Equal(PlayBehavior.Enqueue, directive.PlayBehavior);

            var stream = directive.AudioItem.Stream;

            Assert.Equal("https://url-of-the-stream-to-play", stream.Url);
            Assert.Equal("opaque token representing this stream", stream.Token);
            Assert.Equal("opaque token representing the previous stream", stream.ExpectedPreviousToken);
            Assert.Equal(0, stream.OffsetInMilliseconds);

            var metadata = directive.AudioItem.Metadata;

            Assert.Equal("title of the track to display", metadata.Title);
            Assert.Equal("subtitle of the track to display", metadata.Subtitle);
            Assert.Equal(1, metadata.Art.Sources.Count);
            Assert.Equal("https://url-of-the-album-art-image.png", metadata.Art.Sources.First().Url);
            Assert.Equal(1, metadata.BackgroundImage.Sources.Count);
            Assert.Equal("https://url-of-the-background-image.png", metadata.BackgroundImage.Sources.First().Url);
        }

        [Fact]
        public void DeserializesExampleClearQueueDirectiveJson()
        {
            var deserialized = Utility.ExampleFileContent<IDirective>("ClearQueueDirective.json");

            Assert.Equal(typeof(ClearQueueDirective), deserialized.GetType());

            var directive = (ClearQueueDirective)deserialized;

            Assert.Equal("AudioPlayer.ClearQueue", directive.Type);
            Assert.Equal(ClearBehavior.ClearAll, directive.ClearBehavior);
        }

        [Fact]
        public void DeserializesExampleDialogConfirmIntentJson()
        {
            var deserialized = Utility.ExampleFileContent<IDirective>("DialogConfirmIntent.json");

            Assert.Equal(typeof(DialogConfirmIntent), deserialized.GetType());

            var directive = (DialogConfirmIntent)deserialized;

            Assert.Equal("Dialog.ConfirmIntent", directive.Type);
            Assert.Equal("GetZodiacHoroscopeIntent", directive.UpdatedIntent.Name);
            Assert.Equal(ConfirmationStatus.None, directive.UpdatedIntent.ConfirmationStatus);

            var slot1 = directive.UpdatedIntent.Slots["ZodiacSign"];

            Assert.Equal("ZodiacSign", slot1.Name);
            Assert.Equal("virgo", slot1.Value);
            Assert.Equal(ConfirmationStatus.Confirmed, slot1.ConfirmationStatus);

            var slot2 = directive.UpdatedIntent.Slots["Date"];

            Assert.Equal("Date", slot2.Name);
            Assert.Equal("2015-11-25", slot2.Value);
            Assert.Equal(ConfirmationStatus.Confirmed, slot2.ConfirmationStatus);
        }

        [Fact]
        public void DeserializesExampleDialogConfirmSlotJson()
        {
            var deserialized = Utility.ExampleFileContent<IDirective>("DialogConfirmSlot.json");

            Assert.Equal(typeof(DialogConfirmSlot), deserialized.GetType());

            var directive = (DialogConfirmSlot)deserialized;

            Assert.Equal("Dialog.ConfirmSlot", directive.Type);
            Assert.Equal("Date", directive.SlotName);
            Assert.Equal("GetZodiacHoroscopeIntent", directive.UpdatedIntent.Name);

            var slot1 = directive.UpdatedIntent.Slots["ZodiacSign"];

            Assert.Equal("ZodiacSign", slot1.Name);
            Assert.Equal("virgo", slot1.Value);

            var slot2 = directive.UpdatedIntent.Slots["Date"];

            Assert.Equal("Date", slot2.Name);
            Assert.Equal("2015-11-25", slot2.Value);
            Assert.Equal(ConfirmationStatus.Confirmed, slot2.ConfirmationStatus);
        }

        [Fact]
        public void DeserializesExampleDialogDelegateJson()
        {
            var deserialized = Utility.ExampleFileContent<IDirective>("DialogDelegate.json");

            Assert.Equal(typeof(DialogDelegate), deserialized.GetType());

            var directive = (DialogDelegate)deserialized;

            Assert.Equal("Dialog.Delegate", directive.Type);
            Assert.Equal("GetZodiacHoroscopeIntent", directive.UpdatedIntent.Name);
            Assert.Equal(ConfirmationStatus.None, directive.UpdatedIntent.ConfirmationStatus);

            var slot1 = directive.UpdatedIntent.Slots["ZodiacSign"];

            Assert.Equal("ZodiacSign", slot1.Name);
            Assert.Equal("virgo", slot1.Value);

            var slot2 = directive.UpdatedIntent.Slots["Date"];

            Assert.Equal("Date", slot2.Name);
            Assert.Equal("2015-11-25", slot2.Value);
            Assert.Equal(ConfirmationStatus.Confirmed, slot2.ConfirmationStatus);
        }

        [Fact]
        public void DeserializesExampleDialogElicitSlotJson()
        {
            var deserialized = Utility.ExampleFileContent<IDirective>("DialogElicitSlot.json");

            Assert.Equal(typeof(DialogElicitSlot), deserialized.GetType());

            var directive = (DialogElicitSlot)deserialized;

            Assert.Equal("Dialog.ElicitSlot", directive.Type);
            Assert.Equal("ZodiacSign", directive.SlotName);
            Assert.Equal("GetZodiacHoroscopeIntent", directive.UpdatedIntent.Name);
            Assert.Equal(ConfirmationStatus.None, directive.UpdatedIntent.ConfirmationStatus);

            var slot1 = directive.UpdatedIntent.Slots["ZodiacSign"];

            Assert.Equal("ZodiacSign", slot1.Name);
            Assert.Equal("virgo", slot1.Value);

            var slot2 = directive.UpdatedIntent.Slots["Date"];

            Assert.Equal("Date", slot2.Name);
            Assert.Equal("2015-11-25", slot2.Value);
            Assert.Equal(ConfirmationStatus.Confirmed, slot2.ConfirmationStatus);
        }

        [Fact]
        public void DeserializesExampleDisplayRenderTemplateDirectiveJson()
        {
            var deserialized = Utility.ExampleFileContent<IDirective>("DisplayRenderTemplateDirective.json");

            Assert.Equal(typeof(DisplayRenderTemplateDirective), deserialized.GetType());

            var directive = (DisplayRenderTemplateDirective)deserialized;

            Assert.Equal("Display.RenderTemplate", directive.Type);

            var template = (BodyTemplate1)directive.Template;

            Assert.Equal("BodyTemplate1", template.Type);
            Assert.Equal("A2079", template.Token);
            Assert.Equal("VISIBLE", template.BackButton);
            Assert.Equal("My Favorite Car", template.Title);

            var backgroundImage = template.BackgroundImage;

            Assert.Equal("Textured grey background", backgroundImage.ContentDescription);
            Assert.Equal(1, backgroundImage.Sources.Count);
            Assert.Equal("https://www.example.com/background-image1.png", backgroundImage.Sources.First().Url);

            var textContent = template.Content;

            Assert.Equal("See my favorite car", textContent.Primary.Text);
            Assert.Equal("PlainText", textContent.Primary.Type);
            Assert.Equal("Custom-painted", textContent.Secondary.Text);
            Assert.Equal("PlainText", textContent.Secondary.Type);
            Assert.Equal("By me!", textContent.Tertiary.Text);
            Assert.Equal("PlainText", textContent.Tertiary.Type);
        }

        [Fact]
        public void DeserializesExampleHintDirectiveJson()
        {
            var deserialized = Utility.ExampleFileContent<IDirective>("HintDirective.json");

            Assert.Equal(typeof(HintDirective), deserialized.GetType());

            var directive = (HintDirective)deserialized;

            Assert.Equal("Hint", directive.Type);
            Assert.Equal("PlainText", directive.Hint.Type);
            Assert.Equal("test hint", directive.Hint.Text);
        }

        [Fact]
        public void DeserializesExampleStopDirectiveJson()
        {
            var deserialized = Utility.ExampleFileContent<IDirective>("StopDirective.json");

            Assert.Equal(typeof(StopDirective), deserialized.GetType());

            var directive = (StopDirective)deserialized;

            Assert.Equal("AudioPlayer.Stop", directive.Type);
        }

        [Fact]
        public void DeserializesExampleDirectiveVideoAppDirectiveWithMetadata()
        {
            var deserialized = Utility.ExampleFileContent<IDirective>("VideoAppDirectiveWithMetadata.json");

            Assert.Equal(typeof(VideoAppDirective), deserialized.GetType());

            var directive = (VideoAppDirective)deserialized;

            Assert.Equal("VideoApp.Launch", directive.Type);
            Assert.Equal("https://www.example.com/video/sample-video-1.mp4", directive.VideoItem.Source);
            Assert.Equal("Title for Sample Video", directive.VideoItem.Metadata.Title);
            Assert.Equal("Secondary Title for Sample Video", directive.VideoItem.Metadata.Subtitle);
        }

        [Fact]
        public void DeserializesExampleTemplateBodyTemplate1Json()
        {
            var deserialized = Utility.ExampleFileContent<ITemplate>("TemplateBodyTemplate1.json");

            Assert.Equal(typeof(BodyTemplate1), deserialized.GetType());

            var template = (BodyTemplate1)deserialized;

            Assert.Equal("BodyTemplate1", template.Type);
            Assert.Equal(BackButtonVisibility.Hidden, template.BackButton);

            var textContent = template.Content;

            Assert.Equal("See my favorite car", textContent.Primary.Text);
            Assert.Equal("PlainText", textContent.Primary.Type);
            Assert.Equal("Custom-painted", textContent.Secondary.Text);
            Assert.Equal("PlainText", textContent.Secondary.Type);
            Assert.Equal("By me!", textContent.Tertiary.Text);
            Assert.Equal("PlainText", textContent.Tertiary.Type);
        }

        [Fact]
        public void DeserializesExampleTemplateBodyTemplate2Json()
        {
            var deserialized = Utility.ExampleFileContent<ITemplate>("TemplateBodyTemplate2.json");

            Assert.Equal(typeof(BodyTemplate2), deserialized.GetType());

            var template = (BodyTemplate2)deserialized;

            Assert.Equal("BodyTemplate2", template.Type);
            Assert.Equal("A2079", template.Token);
            Assert.Equal(BackButtonVisibility.Visible, template.BackButton);
            Assert.Equal("My Favorite Car", template.Title);

            var backgroundImage = template.BackgroundImage;

            Assert.Equal("Textured grey background", backgroundImage.ContentDescription);
            Assert.Equal(1, backgroundImage.Sources.Count);
            Assert.Equal("https://www.example.com/background-image1.png", backgroundImage.Sources.First().Url);

            var image = template.Image;

            Assert.Equal("My favorite car", image.ContentDescription);
            Assert.Equal(1, image.Sources.Count);
            Assert.Equal("https://www.example.com/my-favorite-car.png", image.Sources.First().Url);

            var textContent = template.Content;

            Assert.Equal("See my favorite car", textContent.Primary.Text);
            Assert.Equal("PlainText", textContent.Primary.Type);
            Assert.Equal("Custom-painted", textContent.Secondary.Text);
            Assert.Equal("PlainText", textContent.Secondary.Type);
            Assert.Equal("By me!", textContent.Tertiary.Text);
            Assert.Equal("PlainText", textContent.Tertiary.Type);
        }

        [Fact]
        public void DeserializesExampleTemplateBodyTemplate6Json()
        {
            var deserialized = Utility.ExampleFileContent<ITemplate>("TemplateBodyTemplate6.json");

            Assert.Equal(typeof(BodyTemplate6), deserialized.GetType());

            var template = (BodyTemplate6)deserialized;

            Assert.Equal("BodyTemplate6", template.Type);
            Assert.Equal("SampleTemplate_3476", template.Token);
            Assert.Equal(BackButtonVisibility.Visible, template.BackButton);
            Assert.Equal("Sample BodyTemplate6", template.Title);

            var backgroundImage = template.BackgroundImage;

            Assert.Equal("Textured grey background", backgroundImage.ContentDescription);
            Assert.Equal(1, backgroundImage.Sources.Count);
            Assert.Equal("https://www.example.com/background-image1.png", backgroundImage.Sources.First().Url);

            var image = template.Image;

            Assert.Equal("Mount St. Helens landscape", image.ContentDescription);
            Assert.Equal(1, image.Sources.Count);
            Assert.Equal("https://example.com/resources/card-images/mount-saint-helen-small.png", image.Sources.First().Url);

            var textContent = template.Content;

            Assert.Equal("See my favorite car", textContent.Primary.Text);
            Assert.Equal("PlainText", textContent.Primary.Type);
            Assert.Equal("Custom-painted", textContent.Secondary.Text);
            Assert.Equal("PlainText", textContent.Secondary.Type);
            Assert.Equal("By me!", textContent.Tertiary.Text);
            Assert.Equal("PlainText", textContent.Tertiary.Type);
        }

        [Fact]
        public void DeserializesExampleTemplateBodyTemplate7Json()
        {
            var deserialized = Utility.ExampleFileContent<ITemplate>("TemplateBodyTemplate7.json");

            Assert.Equal(typeof(BodyTemplate7), deserialized.GetType());

            var template = (BodyTemplate7)deserialized;

            Assert.Equal("BodyTemplate7", template.Type);
            Assert.Equal("SampleTemplate_3476", template.Token);
            Assert.Equal(BackButtonVisibility.Visible, template.BackButton);
            Assert.Equal("Sample BodyTemplate7", template.Title);

            var backgroundImage = template.BackgroundImage;

            Assert.Equal("Textured grey background", backgroundImage.ContentDescription);
            Assert.Equal(1, backgroundImage.Sources.Count);
            Assert.Equal("https://www.example.com/background-image1.png", backgroundImage.Sources.First().Url);

            var image = template.Image;

            Assert.Equal("Mount St. Helens landscape", image.ContentDescription);
            Assert.Equal(1, image.Sources.Count);
            Assert.Equal("https://example.com/resources/card-images/mount-saint-helen-small.png", image.Sources.First().Url);
        }

        [Fact]
        public void DeserializesExampleTemplateListTemplate1Json()
        {
            var deserialized = Utility.ExampleFileContent<ITemplate>("TemplateListTemplate1.json");

            Assert.Equal(typeof(ListTemplate1), deserialized.GetType());

            var template = (ListTemplate1)deserialized;

            Assert.Equal("ListTemplate1", template.Type);
            Assert.Equal("list_template_one", template.Token);
            Assert.Equal("Pizzas", template.Title);

            var listItems = template.Items;

            Assert.Equal(2, listItems.Count);

            var listItem1 = listItems.First();

            Assert.Equal("item_1", listItem1.Token);
            Assert.Equal("Supreme Large Pan Pizza", listItem1.Image.ContentDescription);
            Assert.Equal(1, listItem1.Image.Sources.Count);
            Assert.Equal("http://www.example.com/images/thumb/SupremePizza1.jpg", listItem1.Image.Sources.First().Url);
            Assert.Equal("RichText", listItem1.Content.Primary.Type);
            Assert.Equal("<font size='7'>Supreme</font> <br/> Large Pan Pizza $17.00", listItem1.Content.Primary.Text);
            Assert.Equal("PlainText", listItem1.Content.Secondary.Type);
            Assert.Equal("Secondary Text", listItem1.Content.Secondary.Text);
            Assert.Equal("PlainText", listItem1.Content.Tertiary.Type);
            Assert.Equal("", listItem1.Content.Tertiary.Text);

            var listItem2 = listItems.Last();

            Assert.Equal("item_2", listItem2.Token);
            Assert.Equal("Meat Eater Large Pan Pizza", listItem2.Image.ContentDescription);
            Assert.Equal(1, listItem2.Image.Sources.Count);
            Assert.Equal("http://www.example.com/images/thumb/MeatEaterPizza1.jpg", listItem2.Image.Sources.First().Url);
            Assert.Equal("RichText", listItem2.Content.Primary.Type);
            Assert.Equal("<font size='7'>Meat Eater</font> <br/> Large Pan Pizza $19.00", listItem2.Content.Primary.Text);
        }

        [Fact]
        public void DeserializesExampleTemplateListTemplate2Json()
        {
            var deserialized = Utility.ExampleFileContent<ITemplate>("TemplateListTemplate2.json");

            Assert.Equal(typeof(ListTemplate2), deserialized.GetType());

            var template = (ListTemplate2)deserialized;

            Assert.Equal("ListTemplate2", template.Type);
            Assert.Equal("A2079", template.Token);
            Assert.Equal(BackButtonVisibility.Visible, template.BackButton);
            Assert.Equal("My Favourite Pizzas", template.Title);

            var backgroundImage = template.BackgroundImage;

            Assert.Equal("Textured grey background", backgroundImage.ContentDescription);
            Assert.Equal(1, backgroundImage.Sources.Count);
            Assert.Equal("https://www.example.com/background-image1.png", backgroundImage.Sources.First().Url);

            var listItems = template.Items;

            Assert.Equal(1, listItems.Count);

            var listItem1 = listItems.First();

            Assert.Equal("item_1", listItem1.Token);
            Assert.Equal("Supreme Large Pan Pizza", listItem1.Image.ContentDescription);
            Assert.Equal(1, listItem1.Image.Sources.Count);
            Assert.Equal("http://www.example.com/images/thumb/SupremePizza1.jpg", listItem1.Image.Sources.First().Url);
            Assert.Equal("RichText", listItem1.Content.Primary.Type);
            Assert.Equal("<font size='7'>Supreme</font> <br/> Large Pan Pizza $17.00", listItem1.Content.Primary.Text);
            Assert.Equal("PlainText", listItem1.Content.Secondary.Type);
            Assert.Equal("Secondary Text", listItem1.Content.Secondary.Text);
            Assert.Equal("PlainText", listItem1.Content.Tertiary.Type);
            Assert.Equal("", listItem1.Content.Tertiary.Text);
        }

        [Fact]
        public void DeserializesExampleResponseJson()
        {
            var deserialized = Utility.ExampleFileContent<SkillResponse>("Response.json");

            Assert.Equal("1.0", deserialized.Version);

            var sessionAttributeSupportedHoriscopePeriods =
                JObject.FromObject(deserialized.SessionAttributes["supportedHoriscopePeriods"]);

            Assert.Equal(true, sessionAttributeSupportedHoriscopePeriods.Value<bool>("daily"));
            Assert.Equal(false, sessionAttributeSupportedHoriscopePeriods.Value<bool>("weekly"));
            Assert.Equal(false, sessionAttributeSupportedHoriscopePeriods.Value<bool>("monthly"));

            var responseBody = deserialized.Response;

            Assert.Equal(false, responseBody.ShouldEndSession);

            var outputSpeech = responseBody.OutputSpeech;

            Assert.Equal(typeof(PlainTextOutputSpeech), outputSpeech.GetType());

            var plainTextOutput = (PlainTextOutputSpeech)outputSpeech;

            Assert.Equal("PlainText", plainTextOutput.Type);
            Assert.Equal("Today will provide you a new learning opportunity. Stick with it and the possibilities will be endless. Can I help you with anything else?", plainTextOutput.Text);

            var card = responseBody.Card;

            Assert.Equal(typeof(SimpleCard), card.GetType());

            var simpleCard = (SimpleCard)card;

            Assert.Equal("Simple", simpleCard.Type);
            Assert.Equal("Horoscope", simpleCard.Title);
            Assert.Equal("Today will provide you a new learning opportunity. Stick with it and the possibilities will be endless.", simpleCard.Content);
        }

        [Fact]
        public void PlainTextConstructorSetsText()
        {
            var plainText = new PlainTextOutputSpeech("testing output");
            Assert.Equal("testing output", plainText.Text);
        }

        [Fact]
        public void SsmlTextConstructorSetsText()
        {
            var output = new Speech(new PlainText("testing output")).ToXml();
            var ssmlText = new SsmlOutputSpeech(output);
            Assert.Equal(output, ssmlText.Ssml);
        }

        [Fact]
        public void NewDirectiveSupport()
        {
            var directive = new JsonDirective("UnknownDirectiveType");
            directive.Properties.Add("testProperty",new JObject(new JProperty("value","test")));
            var jsonOutput = JsonConvert.SerializeObject(directive);

            var outputDirective = JsonConvert.DeserializeObject<IDirective>(jsonOutput);
            var jsonInput = Assert.IsType<JsonDirective>(outputDirective);
            Assert.Equal("UnknownDirectiveType",jsonInput.Type);
            Assert.True(jsonInput.Properties.ContainsKey("testProperty"));
            var jsonObject = Assert.IsType<JObject>(jsonInput.Properties["testProperty"]);
            Assert.Equal("test",jsonObject.Value<string>("value"));
        }

        [Fact]
        public void EmptyDirectiveOrNoOverrideUsesSetValue()
        {
            var tell = ResponseBuilder.Tell("this should end the session");
            Assert.True(tell.Response.ShouldEndSession);

            tell.Response.Directives.Add(new JsonDirective("nothingspecial"));
            Assert.True(tell.Response.ShouldEndSession);
        }

        [Fact]
        public void DirectiveShouldEndSessionOverrideSupport()
        {
            var tell = ResponseBuilder.Tell("this should end the session");
            Assert.True(tell.Response.ShouldEndSession);

            tell.Response.Directives.Add(new VideoAppDirective("https://example.com/test.mp4"));
            Assert.Null(tell.Response.ShouldEndSession);
        }

        [Fact]
        public void MultipleShouldEndDirectivesWithCommonRequirement()
        {
            var tell = ResponseBuilder.Tell("this should end the session");
            Assert.True(tell.Response.ShouldEndSession);

            tell.Response.Directives.Add(new VideoAppDirective("https://example.com/test.mp4"));
            tell.Response.Directives.Add(new VideoAppDirective("https://example.com/test.mp4"));
            Assert.Null(tell.Response.ShouldEndSession);
        }

        [Fact]
        public void ContradictingEndSessionOverrideDefaultsToExplicit()
        {
            var tell = ResponseBuilder.Tell("this should end the session");
            Assert.True(tell.Response.ShouldEndSession);

            //As VideoApp needs a null EndSession and FakeDirective needs false - reverts to explicit
            tell.Response.Directives.Add(new VideoAppDirective("https://example.com/test.mp4"));
            tell.Response.Directives.Add(new FakeDirective());
            Assert.True(tell.Response.ShouldEndSession);
        }

        private class FakeDirective : IEndSessionDirective
        {
            public string Type => "fake";
            public bool? ShouldEndSession => false;
        }

        public void HandlesExampleAskForPermissionsConsentDirective()
        {
            var deserialized = Utility.ExampleFileContent<IDirective>("AskForPermissionsConsentDirective.json");
            var askFor = Assert.IsType<AskForPermissionDirective>(deserialized);
            Assert.Equal(1.ToString(),askFor.Payload.Version);
            Assert.Equal("AskFor",askFor.Name);
            Assert.Equal("alexa::alerts:reminders:skill:readwrite",askFor.Payload.PermissionScope);
            Utility.CompareJson(askFor, "AskForPermissionsConsentDirective.json");
        }

        private bool CompareJson(object actual, JObject expected)
        {
            var actualJObject = JObject.FromObject(actual);
            return JToken.DeepEquals(expected, actualJObject);
        }
    }
}