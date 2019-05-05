using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexa.NET.Request.Type;

namespace Alexa.NET.Request
{
    public static class SkillRequestUtility
    {
        public static string GetIntentName(this SkillRequest request)
        {
            return request.Request is IntentRequest intentRequest ? intentRequest.Intent.Name : string.Empty;
        }

        public static string GetAccountLinkAccessToken(this SkillRequest request)
        {
            return request?.Context?.System?.User?.AccessToken;
        }

        public static string GetApiAccessToken(this SkillRequest request)
        {
            return request?.Context?.System?.ApiAccessToken;
        }

        public static string GetDeviceId(this SkillRequest request)
        {
            return request?.Context?.System?.Device?.DeviceID;
        }

        public static string GetDialogState(this SkillRequest request)
        {
            return request.Request is IntentRequest intentRequest ? intentRequest.DialogState : string.Empty;
        }

        public static Slot GetSlot(this SkillRequest request, string slotName)
        {
            return request.Request is IntentRequest intentRequest && intentRequest.Intent.Slots.ContainsKey(slotName) ? intentRequest.Intent.Slots[slotName] : null;
        }

        public static string GetSlotValue(this SkillRequest request, string slotName)
        {
            return GetSlot(request, slotName)?.Value;
        }

        public static Dictionary<string,object> GetSupportedInterfaces(this SkillRequest request)
        {
            return request?.Context?.System?.Device?.SupportedInterfaces;
        }

        public static bool? IsNewSession(this SkillRequest request)
        {
            return request?.Session?.New;
        }

        public static string GetUserId(this SkillRequest request)
        {
            return request?.Session?.User?.UserId;
        }
    }
}
