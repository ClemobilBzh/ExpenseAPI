using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseAPI.Models.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        public string? CurrencyInfos { get; set; }
    }
}
