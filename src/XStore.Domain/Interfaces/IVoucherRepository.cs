using System.Linq.Expressions;
using XStore.Domain.Entities;

namespace XStore.Domain.Interfaces
{
    public interface IVoucherRepository
    {
        Voucher GetById(Guid id);
        Task<Voucher> GetByIdAsync(Guid id);
        IEnumerable<Voucher> Search(Expression<Func<Voucher, bool>> predicate);
        Task<IEnumerable<Voucher>> SearchAsync(Expression<Func<Voucher, bool>> predicate);
        IEnumerable<Voucher> Search(Expression<Func<Voucher, bool>> predicate,
            int pageNumber,
            int pageSize);
        Voucher Add(Voucher entity);
        Task<Voucher> AddAsync(Voucher entity);
        Voucher Update(Voucher entity);
        void Remove(Guid id);
        void Remove(Expression<Func<Voucher, bool>> expression);
    }
}
