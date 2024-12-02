using Microsoft.EntityFrameworkCore;
using CrudBackend.Models;

namespace CrudBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Record> Records { get; set; }
    }
}
