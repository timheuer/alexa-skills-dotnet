using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Alexa.NET.Request.Type
{
    public enum ErrorType
    {
        [EnumMember(Value = "INVALID_RESPONSE")]
        InvalidResponse,
        [EnumMember(Value = "DEVICE_COMMUNICATION_ERROR")]
        DeviceCommunicationError,
        [EnumMember(Value = "INTERNAL_ERROR")]
        InternalError,
        [EnumMember(Value = "MEDIA_ERROR_UNKNOWN")]
        MediaErrorUnknown,
        [EnumMember(Value = "MEDIA_ERROR_INVALID_REQUEST")]
        InvalidMediaRequest,
        [EnumMember(Value = "MEDIA_ERROR_SERVICE_UNAVAILABLE")]
        MediaServiceUnavailable,
        [EnumMember(Value = "MEDIA_ERROR_INTERNAL_SERVER_ERROR")]
        InternalServerError,
        [EnumMember(Value = "MEDIA_ERROR_INTERNAL_DEVICE_ERROR")]
        InternalDeviceError,
        [EnumMember(Value = "ALREADY_IN_OPERATION")]
        AlreadyInOperationError,
        [EnumMember(Value = "BRIDGE_UNREACHABLE")]
        BridgeUnreachableError,
        [EnumMember(Value = "ENDPOINT_BUSY")]
        EndpointBusyError,
        [EnumMember(Value = "ENDPOINT_LOW_POWER")]
        EndpointLowPowerError,
        [EnumMember(Value = "ENDPOINT_UNREACHABLE")]
        EndpointUnreachableError,
        [EnumMember(Value = "ENDPOINT_CONTROL_UNAVAILABLE")]
        EndpointControlUnavailableError,
        [EnumMember(Value = "EXPIRED_AUTHORIZATION_CREDENTIAL")]
        ExpiredAuthorizationCredentialError,
        [EnumMember(Value = "FIRMWARE_OUT_OF_DATE")]
        FirmwareOutOfDateError,
        [EnumMember(Value = "HARDWARE_MALFUNCTION")]
        HardwareMalfunctionError,
        [EnumMember(Value = "INSUFFICIENT_PERMISSIONS")]
        InsufficientPermissionsError,
        [EnumMember(Value = "INTERNAL_SERVICE_ERROR")]
        InternalServiceError,
        [EnumMember(Value = "INVALID_AUTHORIZATION_CREDENTIAL")]
        InvalidAuthorizationCredentialError,
        [EnumMember(Value = "INVALID_DIRECTIVE")]
        InvalidDirectiveError,
        [EnumMember(Value = "INVALID_VALUE")]
        InvalidValueError,
        [EnumMember(Value = "NO_SUCH_ENDPOINT")]
        NoSuchEndpointError,
        [EnumMember(Value = "NOT_CALIBRATED")]
        NotCalibratedError,
        [EnumMember(Value = "NOT_IN_OPERATION")]
        NotInOperationError,
        [EnumMember(Value = "NOT_SUPPORTED_IN_CURRENT_MODE")]
        NotSupportedInCurrentModeError,
        [EnumMember(Value = "NOT_SUPPORTED_WITH_CURRENT_BATTERY_CHARGE_STATE")]
        NotSupportedWithCurrentBatteryChargeError,
        [EnumMember(Value = "PARTNER_APPLICATION_REDIRECTION")]
        PartnerApplicationRedirectionError,
        [EnumMember(Value = "POWER_LEVEL_NOT_SUPPORTED")]
        PowerLevelNotSupportedError,
        [EnumMember(Value = "RATE_LIMIT_EXCEEDED")]
        RateLimitExceededError,
        [EnumMember(Value = "TEMPERATURE_VALUE_OUT_OF_RANGE")]
        TemperatureValueOutOfRangeError,
        [EnumMember(Value = "TOO_MANY_FAILED_ATTEMPTS")]
        TooManyFailedAttemptsError,
        [EnumMember(Value = "VALUE_OUT_OF_RANGE")]
        ValueOutOfRangeError
    }
}
