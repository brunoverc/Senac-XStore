using Microsoft.EntityFrameworkCore;
using XStore.Infra.Data.Mapping;

namespace XStore.Infra.Data.Context
{
    public class XStoreDbContext : DbContext
    {
        public XStoreDbContext(DbContextOptions<XStoreDbContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new OrderItemMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new VoucherMap());

            base.OnModelCreating(modelBuilder);
        }
    }


}
