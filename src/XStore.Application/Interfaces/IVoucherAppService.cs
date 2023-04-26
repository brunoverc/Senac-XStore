using System.Linq.Expressions;
using XStore.Application.ViewModel;
using XStore.Domain.Entities;

namespace XStore.Application.Interfaces
{
    public interface IVoucherAppService
    {
        Task<VoucherViewModel> AddAsync(VoucherViewModel voucher);
        VoucherViewModel Update(VoucherViewModel voucher);

        void Remove(Guid id);
        void Remove(Expression<Func<Voucher, bool>> expression);

        VoucherViewModel GetById(Guid id);
        Task<VoucherViewModel> GetByIdAsync(Guid id);

        IEnumerable<VoucherViewModel> Search(Expression<Func<Voucher, bool>> predicate);
        Task<IEnumerable<VoucherViewModel>> SearchAsync(Expression<Func<Voucher, bool>> predicate);
        IEnumerable<VoucherViewModel> Search(Expression<Func<Voucher, bool>> predicate,
            int pageNumber,
            int pageSize);
    }
}
