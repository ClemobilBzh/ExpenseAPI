using System;
using System.Text.Json.Serialization;
using ExpenseApi.Models.Enum;

namespace ExpenseApi.Models.DTO
{
    public class ExpenseDto
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExpenseNature Nature { get; set; }

        public string? AmountDisplay { get; set; }

        public AmountDtoIn? Amount { get; set; }

        public string Comment { get; set; }
    }
}
