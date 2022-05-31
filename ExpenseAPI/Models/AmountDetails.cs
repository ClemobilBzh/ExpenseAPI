using System.ComponentModel.DataAnnotations;
using System;

namespace ExpenseApi.Models
{
    public class AmountDetails : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required(ErrorMessage = "Currency must be registered before its use.")]
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }
        public Expense? Expense { get; set; }
        public int ExpenseId { get; set; }

        public string GetAmoutInfo()
        {
            return Currency.Symbol + Amount.ToString("f2");
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
