using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XStore.Domain.Entities;
using XStore.Domain.Shared.Parameters;

namespace XStore.Infra.Data.Mapping
{
    public class VoucherMap : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable(TableNames.TbVoucher);
            builder.Property(v => v.Code).HasMaxLength(6).IsRequired();
            builder.Property(v => v.Percentage).HasPrecision(10, 2);
            builder.Property(v => v.DiscountValue).HasPrecision(10, 2);
            builder.Property(v => v.Amount).IsRequired();
            builder.Property(v => v.DiscountType).IsRequired();
            builder.Property(v => v.Active).HasDefaultValue(true);
            builder.Property(v => v.Used).IsRequired(false);

            builder.Ignore(v => v.Deleted);

        }
    }
}
