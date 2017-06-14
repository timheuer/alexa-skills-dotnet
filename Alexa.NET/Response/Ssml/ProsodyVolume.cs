using System;
namespace Alexa.NET.Response.Ssml
{
    public static class ProsodyVolume
    {
        public const string Silent = "silent";
        public const string ExtraSoft = "x-soft";
        public const string Soft = "soft";
        public const string Medium = "medium";
        public const string Loud = "loud";
        public const string ExtraLoud = "x-loud";


		public static string Decibel(int amount)
		{
			return $"{(amount >= 0 ? "+" : string.Empty)}{amount}dB";
		}
    }
}
