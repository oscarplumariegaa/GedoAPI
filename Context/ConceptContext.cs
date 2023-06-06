using Gedo.Models;
using Microsoft.EntityFrameworkCore;

namespace Gedo.Context
{
    public class ConceptContext : DbContext
    {
        public ConceptContext(DbContextOptions<ConceptContext> options) : base(options) { }

        public DbSet<Concept> Concepts { get; set; }
    }
}
