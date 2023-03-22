using System.Linq.Expressions;
using XStore.Domain.Entities;

namespace XStore.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Order GetById(Guid id);
        Task<Order> GetByIdAsync(Guid id);
        IEnumerable<Order> Search(Expression<Func<Order, bool>> predicate);
        Task<IEnumerable<Order>> SearchAsync(Expression<Func<Order, bool>> predicate);
        IEnumerable<Order> Search(Expression<Func<Order, bool>> predicate,
            int pageNumber,
            int pageSize);
        Order Add(Order entity);
        Task<Order> AddAsync(Order entity);
        Order Update(Order entity);
        void Remove(Guid id);
        void Remove(Expression<Func<Order, bool>> expression);
    }
}
