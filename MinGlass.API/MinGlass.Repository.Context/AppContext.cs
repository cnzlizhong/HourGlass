using Microsoft.EntityFrameworkCore;
using MinGlass.Models;

namespace MinGlass.Repository.Context
{
    public class AppContext: DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
