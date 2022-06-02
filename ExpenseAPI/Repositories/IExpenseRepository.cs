using System;
using ExpenseApi.Models;

namespace ExpenseApi.Repositories
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        Task<IEnumerable<Expense>> GetByUser(int userId, string? sortByDate, string? sortByAmount);
        Task<IEnumerable<Expense>> GetAllSort(string sortByDate);
    }
}
