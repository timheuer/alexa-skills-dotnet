﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Tests
{
    public static class Utility
    {
        private const string ExamplesPath = "Examples";

        public static bool CompareJson(object actual, string expectedFile)
        {
            var actualJObject = JObject.FromObject(actual);
            var expected = ExampleFileContent(expectedFile);
            var expectedJObject = JObject.Parse(expected);
            Console.WriteLine(actualJObject);
            return JToken.DeepEquals(expectedJObject, actualJObject);
        }

        public static string ExampleFileContent(string expectedFile)
        {
            return File.ReadAllText(Path.Combine(ExamplesPath, expectedFile));
        }
    }
}
