using System;
using ExpenseApi.Data;
using ExpenseApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApi.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ExpenseContext context) : base(context)
        {

        }

        public override async Task<User> GetById(int id)
        {
            return await _context.Users
                    .Where(u => u.Id == id)
                    .Include(u => u.Currency)
                    .Include(u => u.Expenses)
                    .SingleOrDefaultAsync();
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
                   .Include(u => u.Currency)
                   .ToListAsync();
        }
    }
}
