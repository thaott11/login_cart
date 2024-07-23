using DemoGH_OnTap.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoGH_OnTap.Configurations
{
    public class GioHangConfig : IEntityTypeConfiguration<GioHang>
    {
        public void Configure(EntityTypeBuilder<GioHang> builder)
        {
           builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Account).WithOne(x => x.GioHang)
                .HasForeignKey<GioHang>(x => x.AccountID); //cấu hình cho mqqh 1-1
        }
    }
}
