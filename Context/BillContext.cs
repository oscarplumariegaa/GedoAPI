using Gedo.Models;
using Microsoft.EntityFrameworkCore;

namespace Gedo.Context
{
    public class BillContext : DbContext
    {
        public BillContext(DbContextOptions<BillContext> options) : base(options) { }

        public DbSet<Bill> Bills { get; set; }
    }
}
