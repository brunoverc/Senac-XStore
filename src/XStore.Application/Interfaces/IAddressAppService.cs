using System.Linq.Expressions;
using XStore.Application.ViewModel;
using XStore.Domain.Entities;

namespace XStore.Application.Interfaces
{
    public interface IAddressAppService
    {
        Task<AddressViewModel> AddAsync(AddressViewModel address);
        AddressViewModel Update(AddressViewModel address);

        void Remove(Guid id);
        void Remove(Expression<Func<Address, bool>> expression);

        AddressViewModel GetById(Guid id);
        Task<AddressViewModel> GetByIdAsync(Guid id);

        IEnumerable<AddressViewModel> Search(Expression<Func<Address, bool>> predicate);
        Task<IEnumerable<AddressViewModel>> SearchAsync(Expression<Func<Address, bool>> predicate);
        IEnumerable<AddressViewModel> Search(Expression<Func<Address, bool>> predicate,
            int pageNumber,
            int pageSize);
    }
}
