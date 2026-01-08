namespace Alexa.NET.Request.SmartHome
{
    /// <summary>
    /// Common directive names used in Alexa Smart Home API
    /// </summary>
    public static class SmartHomeDirectiveNames
    {
        // Discovery
        public const string Discover = "Discover";
        
        // PowerController
        public const string TurnOn = "TurnOn";
        public const string TurnOff = "TurnOff";
        
        // BrightnessController
        public const string SetBrightness = "SetBrightness";
        public const string AdjustBrightness = "AdjustBrightness";
        
        // Response
        public const string Response = "Response";
        public const string ErrorResponse = "ErrorResponse";
    }
}
