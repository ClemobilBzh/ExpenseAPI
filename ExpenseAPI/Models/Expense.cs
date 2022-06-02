using System;
using System.ComponentModel.DataAnnotations;
using ExpenseAPI.Models.Enum;
using static ExpenseAPI.Constants.ErrorMessage;

namespace ExpenseAPI.Models
{
    public class Expense : IValidatableObject
    {
        public int Id { get; set; }

        //Si cette propriété est null au moment de la validation, c'est que le user correspondant à l'Id n'a pas été trouvé
        [Required(ErrorMessage = USER_NOT_REGISTERED)]
        public User User { get; set; }

        public DateTime Date { get; set; }

        public ExpenseNature Nature { get; set; }

        public AmountDetails AmountDetails { get; set; }

        public string Comment { get; set; }

        //Vérification des règles de validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!CurrencyIsConsistent())
            {
                results.Add(new ValidationResult(CURRENCY_NOT_CONSISTANT, new[] { nameof(Models.AmountDetails) }));
            }

            // On ne teste que si l'Id est égal à 0, car ça veut dire que la dépense n'est pas encore enregistrée
            // Sinon, les dépenses enregistrées se comparent avec elle-même et lèvent une erreur
            if (Id == 0 && DoubleExpense())
            {
                results.Add(new ValidationResult(DOUBLE_EXPENSE, new[] { nameof(Expense) }));
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
