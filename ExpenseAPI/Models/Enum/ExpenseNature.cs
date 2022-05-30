using System;
using System.Runtime.Serialization;

namespace ExpenseApi.Models.Enum
{
    public enum ExpenseNature
    {
        [EnumMember(Value = "Restaurant")]
        Restaurant = 1,
        [EnumMember(Value = "Hotel")]
        Hotel = 2,
        [EnumMember(Value = "Transport")]
        Transport = 3,
        [EnumMember(Value = "Misc")]
        Misc = 4
    }
}
