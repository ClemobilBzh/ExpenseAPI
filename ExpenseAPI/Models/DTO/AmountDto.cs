using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseApi.Models.DTO
{
    public class AmountDto : IValidatableObject
    {
        [Range(0.01, 1000000)]
        public float Amount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "CurrencyId must be informed")]
        public int CurrencyId { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
