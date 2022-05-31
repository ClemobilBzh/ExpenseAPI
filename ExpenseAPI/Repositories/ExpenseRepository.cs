using System;

using Microsoft.EntityFrameworkCore;

using ExpenseApi.Data;
using ExpenseApi.Models;

namespace ExpenseApi.Repositories
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(ExpenseContext context) : base(context)
        {
        }

        public override async Task<Expense> GetById(int id)
        {
            Expense expense = await _context.Expenses
                                .Where(e => e.Id == id)
                                .Include(e => e.User)
                                .Include(e => e.AmountDetails)
                                .ThenInclude(a => a.Currency)
                                .SingleOrDefaultAsync();

            return expense;
        }

        public override async Task<IEnumerable<Expense>> GetAll()
        {
            IEnumerable<Expense> expenses = await _context.Expenses
                                .Include(e => e.User)
                                .Include(e => e.AmountDetails)
                                .ThenInclude(a => a.Currency)
                                .ToListAsync();

            return expenses;
        }
    }
}
