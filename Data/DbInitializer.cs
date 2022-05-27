using System;
using ExpenseApi.Models;

namespace ExpenseApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ExpenseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var currencies = new Currency[]
            {
                new Currency {Name = "American Dollar", Symbol = "$"},
                new Currency {Name = "Russian ruble", Symbol = "₽"}
            };
            context.Currencies.AddRange(currencies);
            context.SaveChanges();

            var users = new User[]
            {
                new User {LastName = "Stark", FirstName = "Anthony", CurrencyId = 1 },
                new User {LastName = "Romanova", FirstName = "Natasha", CurrencyId = 2 },
            };
            context.Users.AddRange(users);
            context.SaveChanges();

        }
    }
}
