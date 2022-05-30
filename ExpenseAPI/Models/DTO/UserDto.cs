using System;

namespace ExpenseApi.Models.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CurrencyId { get; set; }

        public string? CurrencyInfos { get; set; }
    }
}