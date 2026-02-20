using Cafeteria.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Backend.Data
{
    public class CafeteriaDbContext : DbContext
    {
        public CafeteriaDbContext(DbContextOptions<CafeteriaDbContext> options) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Rol> Roles { get; set; }
    }
}
