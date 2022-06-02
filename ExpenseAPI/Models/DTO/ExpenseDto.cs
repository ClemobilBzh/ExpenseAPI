using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using static ExpenseAPI.Constants.ErrorMessage;
using static ExpenseAPI.Constants.ValidationRules;
using ExpenseAPI.Models.Enum;

namespace ExpenseAPI.Models.DTO
{
    public class ExpenseDto : IValidatableObject
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        //utilisation de la propriété Range pour vérifier que la valeur n'est pas nulle et positive
        [Range(1, int.MaxValue, ErrorMessage = USER_ID_REQUIRED)]
        public int UserId { get; set; }

        public DateTime Date { get; set; }

        //utilisation de la propriété Range pour vérifier que la valeur n'est pas nulle et positive
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Range(1, int.MaxValue, ErrorMessage = NATURE_ID_REQUIRED)]
        public ExpenseNature Nature { get; set; }

        public string? AmountDisplay { get; set; }

        public AmountDto Amount { get; set; }

        [Required]
        public string Comment { get; set; }

        //Validation des règles sur les dates
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (!DateisNotinFuture())
            {
                results.Add(new ValidationResult(DATE_IN_FUTURE, new[] { nameof(Date) }));
            }
            if (DateisTooOld())
            {
                results.Add(new ValidationResult(String.Format(DATE_TOO_OLD, MAX_RETURN_IN_PAST_IN_MONTHS), new[] { nameof(Date) }));
            }

            return results;
        }

        private bool DateisTooOld()
        {
            DateTime DeadLine = DateTime.Now.AddMonths(-MAX_RETURN_IN_PAST_IN_MONTHS);
            if (Date.Date < DeadLine.Date)
            {
                return true;
            }
            return false;
        }

        private bool DateisNotinFuture()
        {
            if (Date.Date > DateTime.Today)
            {
                return false;
            }
            return true;
        }
    }
}
