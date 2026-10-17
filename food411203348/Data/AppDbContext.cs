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

        // ==========================================
        // 新增這行：把 Category 註冊進實體資料庫中
        // ==========================================
        public DbSet<Category> Categories { get; set; }
    }
}