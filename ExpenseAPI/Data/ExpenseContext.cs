using Microsoft.EntityFrameworkCore;

using ExpenseApi.Models;

namespace ExpenseApi.Data
{
    public class ExpenseContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<AmountDetails> Amounts { get; set; }

        public ExpenseContext(DbContextOptions<ExpenseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AmountDetails>()
                .HasOne(a => a.Expense)
                .WithOne(e => e.Amount)
                .OnDelete(DeleteBehavior.NoAction)
            ;
        }
    }
}
