using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sample1.Models.Domain;

namespace Sample1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }


       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=file::memory:?cache=shared");
            }
        }

        public void InitializeDatabase()
        {
            
           Database.EnsureCreated();   // Create tables
         
            
        }
    }
}
