using System;

using ExpenseAPI.Data;
using ExpenseAPI.Models;

namespace ExpenseAPI.Repositories
{
    public class CurrencyRepository : GenericRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(ExpenseContext context) : base(context)
        {

        }
    }
}
