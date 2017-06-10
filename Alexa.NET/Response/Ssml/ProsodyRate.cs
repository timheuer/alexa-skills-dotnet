using System;
namespace Alexa.NET.Response.Ssml
{
    public static class ProsodyRate
    {
        public const string ExtraSlow = "x-slow";
        public const string Slow = "slow";
        public const string Medium = "medium";
        public const string Fast = "fast";
        public const string ExtraFast = "x-fast";

        public static string Percent(int amount)
        {
            return $"{amount}%";
        }
    }
}
