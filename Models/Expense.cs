using System;
using ExpenseApi.Models.Enum;

namespace ExpenseApi.Models
{
    public class Expense
    {
        public User User { get; set; }
        public DateOnly Date { get; set; }
        public ExpenseNature Nature { get; set; }
        public AmountInContext Amount { get; set; }
        public string Comment { get; set; }
    }
}
