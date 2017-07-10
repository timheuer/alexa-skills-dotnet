using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Alexa.NET.Request
{
    public sealed class IntentSignature
    {
        public string FullName { get; private set; }
        public string Namespace { get; private set; }
        public string Action { get; private set; }
        public System.Collections.ObjectModel.ReadOnlyDictionary<string, IntentProperty> Properties { get; private set; }

		private static Regex PropertyFinder = new Regex(@"(\w+?)@(\w+?)\b(\[(\w+)\])*", RegexOptions.Compiled);

        private IntentSignature(string fullName)
        {
            FullName = fullName;
        }

        public static implicit operator IntentSignature(string action)
        {
            return Parse(action);
        }

        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is string && !string.IsNullOrWhiteSpace((string)obj))
            {
                return FullName.Equals(obj);
            }

            if(obj is IntentSignature)
            {
                return FullName.Equals(((IntentSignature)obj).FullName);
            }

            return base.Equals(obj);
        }

        public static IntentSignature Parse(string action)
        {
            var intentName = new IntentSignature(action);
            Parse(action.Trim(), intentName);
            return intentName;
        }

        private static void Parse(string action, IntentSignature name)
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

        private static void ParseSimple(string action, IntentSignature name)
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

        private static void ParseComplex(string action, IntentSignature name)
        {
            int propertyPoint = action.IndexOf('<');
            ParseSimple(action.Substring(0, propertyPoint),name);

            string propertyPiece = action.Substring(propertyPoint+1, action.Length - (propertyPoint+2));

            IDictionary<string, IntentProperty> propertyDictionary = new Dictionary<string, IntentProperty>();

            foreach (Match match in PropertyFinder.Matches(propertyPiece))
            {
                propertyDictionary.Add(
                    match.Groups[1].Value,
                    new IntentProperty(match.Groups[2].Value,match.Groups[4].Value)
                );
            }

            name.Properties = new System.Collections.ObjectModel.ReadOnlyDictionary<string, IntentProperty>(propertyDictionary);
        }
    }
}
