using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Alexa.NET.Request
{
    public class IntentName
    {
        public string Namespace { get; set; }
        public string Action { get; set; }
        public System.Collections.ObjectModel.ReadOnlyDictionary<string, IntentProperty> Properties { get; set; }

		private static Regex PropertyFinder = new Regex(@"(\w+?)@(\w+?)\b(\[(\w+)\])*", RegexOptions.Compiled);

        public static implicit operator IntentName(string action)
        {
            IntentName name = new IntentName();
            Parse(action.Trim(), name);
            return name;
        }

        public void Parse(string action)
        {
            Parse(action, this);
        }

        private static void Parse(string action, IntentName name)
        {
            if (action.Contains("<") && action.EndsWith(">", StringComparison.Ordinal))
            {
                ParseComplex(action, name);
            }
            else
            {
                ParseSimple(action, name);
            }
        }

        private static void ParseSimple(string action, IntentName name)
        {
			int namespacePoint = action.LastIndexOf('.');

			if (namespacePoint == -1)
			{
				name.Action = action;
				return;
			}

			name.Namespace = action.Substring(0, namespacePoint);
			name.Action = action.Substring(namespacePoint + 1);
        }

        private static void ParseComplex(string action, IntentName name)
        {
            int propertyPoint = action.IndexOf('<');
            ParseSimple(action.Substring(0, propertyPoint),name);

            string propertyPiece = action.Substring(propertyPoint+1, action.Length - (propertyPoint+2));

            Console.WriteLine(propertyPiece);

            IDictionary<string, IntentProperty> propertyDictionary = new Dictionary<string, IntentProperty>();

            foreach (Match match in PropertyFinder.Matches(propertyPiece))
            {
                Console.WriteLine(match.Value);
                propertyDictionary.Add(
                    match.Groups[1].Value,
                    new IntentProperty(match.Groups[2].Value,match.Groups[4].Value)
                );
            }

            name.Properties = new System.Collections.ObjectModel.ReadOnlyDictionary<string, IntentProperty>(propertyDictionary);
        }
    }
}
