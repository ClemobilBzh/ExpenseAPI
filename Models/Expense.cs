using System;
using ExpenseApi.Models.Enum;

namespace ExpenseApi.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public ExpenseNature Nature { get; set; }
        public AmountDetails Amount { get; set; }
        public string Comment { get; set; }
    }
}
