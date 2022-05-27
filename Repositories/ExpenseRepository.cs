using System;
using ExpenseApi.Data;
using ExpenseApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApi.Repositories
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        protected readonly ExpenseContext _context;
        public ExpenseRepository(ExpenseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Expense> GetById(int id)
        {
            Expense expense = await _context.Expenses
                                .Where(e => e.Id == id)
                                .Include(e => e.User)
                                .Include(e => e.Amount)
                                .ThenInclude(a => a.Currency)
                                .SingleOrDefaultAsync();

            return expense;
        }

        public override async Task<IEnumerable<Expense>> GetAll()
        {
            IEnumerable<Expense> expenses = await _context.Expenses
                                .Include(e => e.User)
                                .Include(e => e.Amount)
                                .ThenInclude(a => a.Currency)
                                .ToListAsync();

            return expenses;
        }

        public async override Task Remove(Expense expense)
        {
            try
            {
                if (expense.Amount != null)
                {
                    _context.Entry(expense.Amount).State = EntityState.Deleted;
                    _context.Amounts.Remove(expense.Amount);
                    await _context.SaveChangesAsync();
                }
                await base.Remove(expense);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

        }
    }
}
