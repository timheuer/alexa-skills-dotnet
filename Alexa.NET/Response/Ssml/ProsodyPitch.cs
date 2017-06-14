using System;
namespace Alexa.NET.Response.Ssml
{
    public static class ProsodyPitch
    {
        public const string ExtraLow = "x-low";
        public const string Low = "low";
        public const string Medium = "medium";
        public const string High = "high";
        public const string ExtraHigh = "x-high";

        public static string Percent(int amount)
        {
            return $"{(amount >= 0 ? "+" : string.Empty)}{amount}%";
        }
    }
}
