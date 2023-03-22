using System.Linq.Expressions;
using XStore.Domain.Entities;

namespace XStore.Domain.Interfaces
{
    public interface IClientRepository
    {
        Client GetById(Guid id);
        Task<Client> GetByIdAsync(Guid id);
        IEnumerable<Client> Search(Expression<Func<Client, bool>> predicate);
        Task<IEnumerable<Client>> SearchAsync(Expression<Func<Client, bool>> predicate);
        IEnumerable<Client> Search(Expression<Func<Client, bool>> predicate,
            int pageNumber,
            int pageSize);
        Client Add(Client entity);
        Task<Client> AddAsync(Client entity);
        Client Update(Client entity);
        void Remove(Guid id);
        void Remove(Expression<Func<Client, bool>> expression);
    }
}
