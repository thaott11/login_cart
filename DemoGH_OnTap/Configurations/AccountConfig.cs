using DemoGH_OnTap.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoGH_OnTap.Configurations
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
           builder.HasKey(x => x.Id);
            builder.HasOne(x => x.GioHang).WithOne(x => x.Account)
                .HasForeignKey<GioHang>(x =>x.AccountID); // cáu hình mối quan hệ 1-1
        }
    }
}
