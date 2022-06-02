using System.ComponentModel.DataAnnotations;
using System;

using static ExpenseAPI.Constants.ErrorMessage;

namespace ExpenseAPI.Models
{
    public class AmountDetails : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public float Amount { get; set; }

        //Si cette propriété est null au moment de la validation, c'est que la devise Currency correspondant à l'Id n'a pas été trouvée
        [Required(ErrorMessage = CURRENCY_NOT_REGISTERED)]
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
