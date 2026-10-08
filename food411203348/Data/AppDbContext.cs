using Microsoft.EntityFrameworkCore;
using food411203348.Models;

namespace food411203348.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<FoodItem> FoodItems { get; set; }
    }
}