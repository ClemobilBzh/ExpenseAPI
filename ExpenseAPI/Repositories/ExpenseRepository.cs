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

        public async Task<IEnumerable<Expense>> GetAllSort(string? sortByDate)
        {
            IEnumerable<Expense> expenses = await _context.Expenses
                                                .Include(e => e.User)
                                                .Include(e => e.AmountDetails)
                                                .ThenInclude(a => a.Currency)
                                                .ToListAsync();

            if (sortByDate != null)
            {
                expenses = SortbyDate(expenses, sortByDate);
            }

            return expenses;
        }

        public async Task<IEnumerable<Expense>> GetByUser(int userId, string? sortByDate, string? sortByAmount)
        {
            IEnumerable<Expense> expenses = await _context.Expenses
                                .Where(e => e.User.Id == userId)
                                .Include(e => e.User)
                                .Include(e => e.AmountDetails)
                                .ThenInclude(a => a.Currency)
                                .ToListAsync();

            if (sortByDate != null)
            {
                expenses = SortbyDate(expenses, sortByDate);
            }

            if (sortByAmount != null)
            {
                expenses = SortbyAmount(expenses, sortByDate);
            }

            return expenses;
        }

        private static IEnumerable<Expense> SortbyDate(IEnumerable<Expense> expenses, string? sortByDate)
        {
            if (sortByDate != null && sortByDate.ToLower() == "asc")
            {
                expenses = expenses.OrderBy(e => e.Date);
            }
            else if (sortByDate != null && sortByDate.ToLower() == "desc")
            {
                expenses = expenses.OrderByDescending(e => e.Date);
            }

            return expenses;
        }

        private static IEnumerable<Expense> SortbyAmount(IEnumerable<Expense> expenses, string? sortByAmount)
        {
            if (sortByAmount != null && sortByAmount.ToLower() == "asc")
            {
                expenses = expenses.OrderBy(e => e.AmountDetails.Amount);
            }
            else if (sortByAmount != null && sortByAmount.ToLower() == "desc")
            {
                expenses = expenses.OrderByDescending(e => e.AmountDetails.Amount);
            }

            return expenses;
        }
    }
}
