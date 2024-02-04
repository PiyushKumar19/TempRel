using Microsoft.EntityFrameworkCore;

namespace TempRel.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Model1> Model1s { get; set; }
        public DbSet<Model2> Model2s { get; set; }
        public DbSet<TempModel> TempModel { get; set; }
    }
}
