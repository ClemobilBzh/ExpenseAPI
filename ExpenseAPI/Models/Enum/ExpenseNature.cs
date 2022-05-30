using System;
using System.Runtime.Serialization;

namespace ExpenseApi.Models.Enum
{
    public enum ExpenseNature
    {
        [EnumMember(Value = "Restaurant")]
        Restaurant,
        [EnumMember(Value = "Hotel")]
        Hotel,
        [EnumMember(Value = "Transport")]
        Transport,
        [EnumMember(Value = "Misc")]
        Misc
    }
}
