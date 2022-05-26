using System;

namespace ExpenseApi.Models.DTO
{
    public class AmountDtoIn
    {
        public float Amount { get; set; }
        public int CurrencyId { get; set; }
    }
}
