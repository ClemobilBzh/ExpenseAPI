using System;

namespace ExpenseApi.Models
{
    public class AmountInContext
    {
        public float Amount { get; set; }
        public Currency Currency { get; set; }

        public override string ToString()
        {
            return Currency.Symbol + Amount.ToString("D2");
        }
    }
}
