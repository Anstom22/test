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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Difficulties
            // Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = 1,
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = 2,
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = 3,
                    Name = "Hard"
                }
            };

            // Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);



            // Seed data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = 1,
                    Name = "Auckland",
                    Code = "AKL"
                },
                new Region
                {
                    Id = 2,
                    Name = "Northland",
                    Code = "NTL"
                },
                new Region
                {
                    Id = 3,
                    Name = "Bay Of Plenty",
                    Code = "BOP"
                },
                new Region
                {
                    Id = 4,
                    Name = "Wellington",
                    Code = "WGN"
                },
                
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
