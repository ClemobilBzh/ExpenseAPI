using System;
using System.ComponentModel.DataAnnotations;
using ExpenseApi.Models.Enum;

namespace ExpenseApi.Models
{
    public class Expense : IValidatableObject
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public ExpenseNature Nature { get; set; }
        public AmountDetails Amount { get; set; }
        public string Comment { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            return results;
        }
    }
}
