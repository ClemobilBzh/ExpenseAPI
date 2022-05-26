using System;
using ExpenseApi.Models.Enum;

namespace ExpenseApi.Models.DTO
{
    public class ExpenseDtoOut
    {
        public string UserName { get; set; }

        public DateTime Date { get; set; }

        public ExpenseNature Nature { get; set; }

        public string Amount { get; set; }
        public string Comment { get; set; }
    }
}
