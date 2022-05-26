using System;
using ExpenseApi.Models.Enum;

namespace ExpenseApi.Models.DTO
{
    public class ExpenseDtoIn
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public ExpenseNature Nature { get; set; }
        public AmountDtoIn Amount { get; set; }
        public string Comment { get; set; }
    }
}
