using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{
    public class BulkyDbContext : DbContext
    {
        public BulkyDbContext(DbContextOptions options) : base(options)
        {

        }
        DbSet<Category> categories { get; set; }
    }
}
