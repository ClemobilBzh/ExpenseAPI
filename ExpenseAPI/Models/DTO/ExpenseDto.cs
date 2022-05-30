using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using ExpenseApi.Models.Enum;

namespace ExpenseApi.Models.DTO
{
    public class ExpenseDto : IValidatableObject
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "UserId must be informed")]
        public int UserId { get; set; }

        public DateTime Date { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Range(1, int.MaxValue, ErrorMessage = "Nature must be informed")]
        public ExpenseNature Nature { get; set; }

        public string? AmountDisplay { get; set; }

        public AmountDto Amount { get; set; }

        [Required]
        public string Comment { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (!DateisNotinFuture())
            {
                results.Add(new ValidationResult("Date cannot be in the future.", new[] { nameof(Date) }));
            }
            if (DateisTooOld())
            {
                results.Add(new ValidationResult("Date cannot be older than 3 months.", new[] { nameof(Date) }));
            }

            return results;
        }

        private bool DateisTooOld()
        {
            DateTime DeadLine = DateTime.Now.AddMonths(-3);
            if (Date.Date < DeadLine.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DateisNotinFuture()
        {
            if (Date.Date > DateTime.Today)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
