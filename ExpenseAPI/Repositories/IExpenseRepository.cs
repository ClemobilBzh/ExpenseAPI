using System;
using ExpenseAPI.Models;

namespace ExpenseAPI.Repositories
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        Task<IEnumerable<Expense>> GetByUser(int userId, string? sortByDate, string? sortByAmount);
        Task<IEnumerable<Expense>> GetAllSort(string sortByDate);
    }
}
