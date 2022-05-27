using System;
using ExpenseApi.Data;
using ExpenseApi.Models;

namespace ExpenseApi.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ExpenseContext context) : base(context)
        {

        }
    }
}
