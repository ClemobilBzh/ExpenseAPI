using System;

namespace ExpenseApi.Models
{
    public class AmountDetails
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }
        public Expense Expense { get; set; }
        public int ExpenseId { get; set; }

        public string GetAmoutInfo()
        {
            return Currency.Symbol + Amount.ToString("f2");
        }
    }
}
