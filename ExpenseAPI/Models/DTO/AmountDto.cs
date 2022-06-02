using System;
using System.ComponentModel.DataAnnotations;
using static ExpenseAPI.Constants.ErrorMessage;

namespace ExpenseAPI.Models.DTO
{
    public class AmountDto : IValidatableObject
    {
        //utilisation de la propriété Range pour vérifier que la valeur n'est pas nulle et positive
        [Range(0.01, 1000000)]
        public float Amount { get; set; }

        //utilisation de la propriété Range pour vérifier que la valeur n'est pas nulle et positive
        [Range(1, int.MaxValue, ErrorMessage = CURRENCY_REQUIRED)]
        public int CurrencyId { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
