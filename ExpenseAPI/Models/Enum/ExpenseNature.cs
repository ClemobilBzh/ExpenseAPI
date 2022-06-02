using System;
using System.Runtime.Serialization;

namespace ExpenseAPI.Models.Enum
{
    /// Les types de dépenses ont été enregistrés ici en dur, 
    /// mais il serait intéressant de le stocker en base de données pour qu'ils puissent être gérés par l'utilisateur
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
