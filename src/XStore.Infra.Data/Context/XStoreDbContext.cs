using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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

            //modelBuilder.ShadowProperties();

            base.OnModelCreating(modelBuilder);
        }

        //private void SetGlobalQueryFilters(ModelBuilder modelBuilder)
        //{
        //    foreach(var tp in modelBuilder.Model.GetEntityTypes())
        //    {
        //        var t = tp.ClrType;
        //        if(typeof(ISoftDelete).IsAssignableFrom(t))
        //        {
        //            var method = SetGlobalQueryForSoftDeleteMethodInfo.MakeGenericMethod(t);
        //            method.Invoke(this, new object[] { modelBuilder });
        //        }
        //    }
        //}
    }

    //private static readonly MethodInfo SetGlobalQueryForSoftDeleteMethodInfo = typeof(XStoreDbContext)
    //    .GetMethods(BindingFlags.Public | BindingFlags.Instance)
    //    .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForSoftDelete");

    //public void SetGlobalQueryForSoftDelete<T>(ModelBuilder builder) where T : class, ISoftDelete
    //{
    //    builder.Entity<T>.HasQueryFilter(item => !EF.Property<bool>(item, "Deleted"));
    //}

}
