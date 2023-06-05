using Gedo.Models;
using Microsoft.EntityFrameworkCore;

namespace Gedo.Context
{
    public class BudgetContext : DbContext
    {
        public BudgetContext(DbContextOptions<BudgetContext> options) : base(options) { }

        public DbSet<Budget> Budgets { get; set; }
    }
}
