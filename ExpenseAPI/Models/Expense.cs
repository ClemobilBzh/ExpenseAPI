using System;
using System.ComponentModel.DataAnnotations;
using ExpenseApi.Models.Enum;

namespace ExpenseApi.Models
{
    public class Expense : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User must be registered before its use.")]
        public User User { get; set; }

        public DateTime Date { get; set; }

        public ExpenseNature Nature { get; set; }

        public AmountDetails AmountDetails { get; set; }

        public string Comment { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!CurrencyIsConsistent())
            {
                results.Add(new ValidationResult("Expense currency is inconsistent with user currency.", new[] { nameof(Models.AmountDetails) }));
            }

            if (Id == 0 && DoubleExpense())
            {
                results.Add(new ValidationResult("Expense has been already registered.", new[] { nameof(Expense) }));
            }

            return results;
        }

        private bool DoubleExpense()
        {
            if (User.Expenses.Any(e => e.Date == Date
                                && e.AmountDetails.Amount == AmountDetails.Amount))
            {
                return true;
            }
            return false;
        }

        private bool CurrencyIsConsistent()
        {
            if (AmountDetails.Currency.Id == User.Currency.Id)
            {
                return true;
            }
            return false;
        }
    }
}
