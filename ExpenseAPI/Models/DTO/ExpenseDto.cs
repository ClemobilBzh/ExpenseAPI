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

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
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
            return results;
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
