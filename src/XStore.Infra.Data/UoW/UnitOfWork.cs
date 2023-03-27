using XStore.Domain.Shared.Transaction;

namespace XStore.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context.XStoreDbContext _context;

        public UnitOfWork(Context.XStoreDbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
