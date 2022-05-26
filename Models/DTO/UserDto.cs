using System;

namespace ExpenseApi.Models.DTO
{
    public class UserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CurrencyId { get; set; }
    }
}
