using System;
using ExpenseApi.Data;
using ExpenseApi.Models;

namespace ExpenseApi.Repositories
{
    public class CurrencyRepository : GenericRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(ExpenseContext context) : base(context)
        {

        }
    }
}
