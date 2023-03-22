using System.Linq.Expressions;
using XStore.Domain.Entities;

namespace XStore.Domain.Interfaces
{
    public interface IAddressRepository
    {
        Address GetById(Guid id);
        Task<Address> GetByIdAsync(Guid id);
        IEnumerable<Address> Search(Expression<Func<Address, bool>> predicate);
        Task<IEnumerable<Address>> SearchAsync(Expression<Func<Address, bool>> predicate);
        IEnumerable<Address> Search(Expression<Func<Address, bool>> predicate,
            int pageNumber,
            int pageSize);
        Address Add(Address entity);
        Task<Address> AddAsync(Address entity);
        Address Update(Address entity);
        void Remove(Guid id);
        void Remove(Expression<Func<Address, bool>> expression);
    }
}
