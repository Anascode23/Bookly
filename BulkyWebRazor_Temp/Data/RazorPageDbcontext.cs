using BulkyWebRazor_Temp.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebRazor_Temp.Data
{
    public class RazorPageDbContext : DbContext
    {
        public RazorPageDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Mystery", DisplayOrder = 2 },
                new Category { Id = 3, Name = "SciFi", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Comedy", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Adventure", DisplayOrder = 5 }
                );
        }
    }
}
