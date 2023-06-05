using Gedo.Models;
using Microsoft.EntityFrameworkCore;

namespace Gedo.Context
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
    }
}
