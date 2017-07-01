using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Alexa.NET.Response.Directive;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Sdk;

namespace Alexa.NET.Tests
{
    public class RenderTemplateTests
    {
        private const string ExamplesPath = "Examples";
        //Examples found at: https://developer.amazon.com/public/solutions/alexa/alexa-skills-kit/docs/display-interface-reference#configure-your-skill-for-the-display-directive
        
        [Fact]
        public void Creates_RenderTemplateDirective()
        {
            var actual = new DisplayRenderTemplateDirective();
            Assert.True(CompareJson(actual,"DisplayRenderTemplateDirective.json"));
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
