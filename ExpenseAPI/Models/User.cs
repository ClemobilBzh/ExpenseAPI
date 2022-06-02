using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

using static ExpenseAPI.Constants.ErrorMessage;

namespace ExpenseAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        //Si cette propriété est null au moment de la validation, c'est que le user correspondant à l'Id n'a pas été trouvé
        [Required(ErrorMessage = CURRENCY_NOT_REGISTERED)]
        public Currency Currency { get; set; }

        public int CurrencyId { get; set; }

        public List<Expense>? Expenses { get; set; }

        public string GetName()
        {
            StringBuilder sb = new();

            sb.Append(FirstName);
            sb.Append(' ');
            sb.Append(LastName);

            return sb.ToString();
        }
    }
}
