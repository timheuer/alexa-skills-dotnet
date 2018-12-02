using System;
using System.Collections.Generic;
using System.IO;
using Alexa.NET.Response.Directive;
using Alexa.NET.Response.Directive.Templates;
using Alexa.NET.Response.Directive.Templates.Types;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Alexa.NET.Tests
{
    public class RenderTemplateTests
    {
        private const string ExamplesPath = "Examples";
        private const string ImageSource = "https://example.com/resources/card-images/mount-saint-helen-small.png";
        private const string ImageDescription = "Mount St. Helens landscape";
        //Examples found at: https://developer.amazon.com/public/solutions/alexa/alexa-skills-kit/docs/display-interface-reference#configure-your-skill-for-the-display-directive

        [Fact]
        public void Creates_RenderTemplateDirective()
        {
            var actual = new DisplayRenderTemplateDirective
            {
                Template = new BodyTemplate1
                {
                    Token = "A2079",
                    BackButton = BackButtonVisibility.Visible,
                    BackgroundImage = new TemplateImage
                    {
                        ContentDescription = "Textured grey background",
                        Sources = new List<ImageSource>
                        {
                            new ImageSource { Url="https://www.example.com/background-image1.png"}
                        }
                    },
                    Title = "My Favorite Car",
                    Content = new TemplateContent
                    {
                        Primary = new TemplateText { Text = "See my favorite car", Type = TextType.Plain },
                        Secondary = new TemplateText { Text = "Custom-painted", Type = TextType.Plain },
                        Tertiary = new TemplateText { Text = "By me!", Type = TextType.Plain }
                    }
                }
            };
            Assert.True(CompareJson(actual, "DisplayRenderTemplateDirective.json"));
        }

        [Fact]
        public void Create_BodyTemplate1()
        {
            var actual = new BodyTemplate1
            {
                BackButton = BackButtonVisibility.Hidden,
                Content = new TemplateContent
                {
                    Primary = new TemplateText { Text = "See my favorite car", Type = TextType.Plain },
                    Secondary = new TemplateText { Text = "Custom-painted", Type = TextType.Plain },
                    Tertiary = new TemplateText { Text = "By me!", Type = TextType.Plain }
                }
            };
            Assert.True(CompareJson(actual, "TemplateBodyTemplate1.json"));
        }

        [Fact]
        public void Create_BodyTemplate2()
        {
            var actual = new BodyTemplate2
            {
                Token = "A2079",
                BackButton = BackButtonVisibility.Visible,
                Image = new TemplateImage
                {
                    ContentDescription = "My favorite car",
                    Sources = new List<ImageSource>
                    {
                        new ImageSource{Url="https://www.example.com/my-favorite-car.png"}
                    }
                },
                BackgroundImage = new TemplateImage
                {
                    ContentDescription = "Textured grey background",
                    Sources = new List<ImageSource>
                    {
                        new ImageSource { Url="https://www.example.com/background-image1.png"}
                    }
                },


                Title = "My Favorite Car",
                Content = new TemplateContent
                {
                    Primary = new TemplateText { Text = "See my favorite car", Type = TextType.Plain },
                    Secondary = new TemplateText { Text = "Custom-painted", Type = TextType.Plain },
                    Tertiary = new TemplateText { Text = "By me!", Type = TextType.Plain }
                }
            };
            Assert.True(CompareJson(actual, "TemplateBodyTemplate2.json"));
        }

        [Fact]
        public void Create_BodyTemplate6()
        {
            var actual = new BodyTemplate6
            {
                Token = "SampleTemplate_3476",
                BackButton = BackButtonVisibility.Visible,
                Image = new TemplateImage
                {
                    ContentDescription = ImageDescription,
                    Sources = new List<ImageSource> { new ImageSource { Url = ImageSource } }
                },
                BackgroundImage = new TemplateImage
                {
                    ContentDescription = "Textured grey background",
                    Sources = new List<ImageSource>
                    {
                        new ImageSource { Url="https://www.example.com/background-image1.png"}
                    }
                },


                Title = "Sample BodyTemplate6",
                Content = new TemplateContent
                {
                    Primary = new TemplateText { Text = "See my favorite car", Type = TextType.Plain },
                    Secondary = new TemplateText { Text = "Custom-painted", Type = TextType.Plain },
                    Tertiary = new TemplateText { Text = "By me!", Type = TextType.Plain }
                }
            };
            Assert.True(CompareJson(actual, "TemplateBodyTemplate6.json"));
        }

        [Fact]
        public void Create_BodyTemplate7()
        {
            var actual = new BodyTemplate7
            {
                Token = "SampleTemplate_3476",
                BackButton = BackButtonVisibility.Visible,
                Image = new TemplateImage
                {
                    ContentDescription = ImageDescription,
                    Sources = new List<ImageSource> { new ImageSource { Url = ImageSource } }
                },
                BackgroundImage = new TemplateImage
                {
                    ContentDescription = "Textured grey background",
                    Sources = new List<ImageSource>
                    {
                        new ImageSource { Url="https://www.example.com/background-image1.png"}
                    }
                },


                Title = "Sample BodyTemplate7"
            };
            Assert.True(CompareJson(actual, "TemplateBodyTemplate7.json"));
        }

        [Fact]
        public void Create_ListTemplate1()
        {
            var actual = new ListTemplate1
            {
                Token = "list_template_one",
                Title = "Pizzas",
                Items = new List<ListItem>
                {
                    new ListItem
                    {
                        Token="item_1",
                        Content = new TemplateContent
                        {
                            Primary = new TemplateText
                            {
                                Text="<font size='7'>Supreme</font> <br/> Large Pan Pizza $17.00",
                                Type=TextType.Rich
                            },
                            Secondary = new TemplateText
                            {
                                Text="Secondary Text",
                                Type=TextType.Plain
                            },
                            Tertiary=new TemplateText
                            {
                                Text=string.Empty,
                                Type=TextType.Plain
                            }
                        },
                        Image = new TemplateImage
                        {
                            Sources = new List<ImageSource>{new ImageSource
                            {
                                Url= "http://www.example.com/images/thumb/SupremePizza1.jpg"
                            }},
                            ContentDescription = "Supreme Large Pan Pizza"
                        }
                    },
                    new ListItem
                    {
                        Token="item_2",
                        Content = new TemplateContent
                        {
                            Primary = new TemplateText
                            {
                                Text="<font size='7'>Meat Eater</font> <br/> Large Pan Pizza $19.00",
                                Type=TextType.Rich
                            }
                        },
                        Image = new TemplateImage
                        {
                            Sources = new List<ImageSource>{new ImageSource
                            {
                                Url= "http://www.example.com/images/thumb/MeatEaterPizza1.jpg"
                            }},
                            ContentDescription = "Meat Eater Large Pan Pizza"
                        }
                    },
                }
            };
            Assert.True(CompareJson(actual, "TemplateListTemplate1.json"));
        }

        [Fact]
        public void Create_ListTemplate2()
        {
            var actual = new ListTemplate2
            {
                Token = "A2079",
                Title = "My Favourite Pizzas",
                BackButton = BackButtonVisibility.Visible,
                BackgroundImage = new TemplateImage
                {
                    ContentDescription = "Textured grey background",
                    Sources = new List<ImageSource>
                    {
                        new ImageSource { Url="https://www.example.com/background-image1.png"}
                    }
                },
                Items = new List<ListItem>
                {
                    new ListItem
                    {
                        Token="item_1",
                        Content = new TemplateContent
                        {
                            Primary = new TemplateText
                            {
                                Text="<font size='7'>Supreme</font> <br/> Large Pan Pizza $17.00",
                                Type=TextType.Rich
                            },
                            Secondary = new TemplateText
                            {
                                Text="Secondary Text",
                                Type=TextType.Plain
                            },
                            Tertiary=new TemplateText
                            {
                                Text=string.Empty,
                                Type=TextType.Plain
                            }
                        },
                        Image = new TemplateImage
                        {
                            Sources = new List<ImageSource>{new ImageSource
                            {
                                Url= "http://www.example.com/images/thumb/SupremePizza1.jpg"
                            }},
                            ContentDescription = "Supreme Large Pan Pizza"
                        }
                    }
                }
            };
            Assert.True(CompareJson(actual, "TemplateListTemplate2.json"));
        }

        [Fact]
        public void Create_Basic_Image()
        {
            var actual = new TemplateImage
            {
                ContentDescription = ImageDescription,
                Sources = new List<ImageSource> { new ImageSource { Url = ImageSource } }
            };
            Assert.True(CompareJson(actual, "TemplateImageBasic.json"));
        }

        [Fact]
        public void Create_Image()
        {
            var actual = new TemplateImage
            {
                ContentDescription = ImageDescription,
                Sources = new List<ImageSource> { new ImageSource {
                        Url = ImageSource,
                        Size = ImageSize.Small,
                        Height=480,
                        Width=640
                    }
                }
            };
            Assert.True(CompareJson(actual, "TemplateImage.json"));
        }

        [Fact]
        public void HintCreatesCorrectJson()
        {
            var hint = new HintDirective("test hint");
            Assert.True(CompareJson(hint, "HintDirective.json"));
        }

        private bool CompareJson(object actual, string expectedFile)
        {

            var actualJObject = JObject.FromObject(actual);
            var expected = File.ReadAllText(Path.Combine(ExamplesPath, expectedFile));
            var expectedJObject = JObject.Parse(expected);
            Console.WriteLine(expected);
            Console.WriteLine(actualJObject);
            return JToken.DeepEquals(expectedJObject, actualJObject);
        }
    }
}
