using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Alexa.NET.Response.Directive;
using Alexa.NET.Response.Directive.Templates;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Sdk;

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
            var actual = new DisplayRenderTemplateDirective();
            Assert.True(CompareJson(actual,"DisplayRenderTemplateDirective.json"));
        }

        [Fact]
        public void Create_BodyTemplate1()
        {
            var actual = new DisplayRenderTemplateDirective
            {

            };
            Assert.True(CompareJson(actual, "TemplateBodyTemplate1.json"));
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
                        Height=640,
                        Width=480
                    } 
                }
            };
            Assert.True(CompareJson(actual, "TemplateImage.json"));
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
