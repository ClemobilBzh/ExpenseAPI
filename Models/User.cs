using System;

namespace ExpenseApi.Models
{
    public class User
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Currency Currency { get; set; }
    }
}
