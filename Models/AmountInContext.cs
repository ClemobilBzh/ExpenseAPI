using System;

namespace ExpenseApi.Models
{
    public class AmountInContext
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }
        public Expense Expense { get; set; }
        public int ExpenseId { get; set; }

        public override string ToString()
        {
            return Currency.Symbol + Amount.ToString("D2");
        }
    }
}
