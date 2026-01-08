namespace Alexa.NET.Response.SmartHome
{
    /// <summary>
    /// Common error types for Alexa Smart Home API
    /// </summary>
    public static class SmartHomeErrorTypes
    {
        public const string BridgeUnreachable = "BRIDGE_UNREACHABLE";
        public const string EndpointBusy = "ENDPOINT_BUSY";
        public const string EndpointLowPower = "ENDPOINT_LOW_POWER";
        public const string EndpointUnreachable = "ENDPOINT_UNREACHABLE";
        public const string ExpiredAuthorizationCredential = "EXPIRED_AUTHORIZATION_CREDENTIAL";
        public const string FirmwareOutOfDate = "FIRMWARE_OUT_OF_DATE";
        public const string HardwareMalfunction = "HARDWARE_MALFUNCTION";
        public const string InsufficientPermissions = "INSUFFICIENT_PERMISSIONS";
        public const string InternalError = "INTERNAL_ERROR";
        public const string InvalidAuthorizationCredential = "INVALID_AUTHORIZATION_CREDENTIAL";
        public const string InvalidDirective = "INVALID_DIRECTIVE";
        public const string InvalidValue = "INVALID_VALUE";
        public const string NoSuchEndpoint = "NO_SUCH_ENDPOINT";
        public const string NotSupportedInCurrentMode = "NOT_SUPPORTED_IN_CURRENT_MODE";
        public const string RateLimitExceeded = "RATE_LIMIT_EXCEEDED";
        public const string TemperatureValueOutOfRange = "TEMPERATURE_VALUE_OUT_OF_RANGE";
        public const string ValueOutOfRange = "VALUE_OUT_OF_RANGE";
    }
}
