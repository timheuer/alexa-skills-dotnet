using System;
namespace Alexa.NET.Response
{
    public static class RequestedPermission
    {
        public const string ReadHouseholdList  = "alexa::household:lists:read";
        public const string WriteHouseholdList = "alexa::household:lists:write";
        public const string FullAddress = "read::alexa:device:all:address";
        public const string AddressCountryAndPostalCode = "read::alexa:device:all:address:country_and_postal_code";
    }
}
