using Microsoft.EntityFrameworkCore;

namespace DemoGH_OnTap.Models
{
    public class SD18406CartDbContext : DbContext
    {
        public SD18406CartDbContext(DbContextOptions options) : base(options)
        {
        }
        //tạo các dbset
        public DbSet<Account> Accounts { get; set; }
        public DbSet<GioHang> GioHang { get; set;}
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<GHCT> GHCTs { get; set; }
    }
}
