using System.Runtime.Serialization;

namespace Alexa.NET.Response.Directive
{
    public enum UpdateBehavior
    {
        [EnumMember(Value = "REPLACE")]
        Replace,
        [EnumMember(Value = "CLEAR")]
        Clear
    }
}