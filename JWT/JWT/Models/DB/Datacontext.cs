using Microsoft.EntityFrameworkCore;

namespace JWT.Models.DB
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions ops) : base(ops) 
        {
        }
        public DbSet<User> Users { get; set; }

    }
}
