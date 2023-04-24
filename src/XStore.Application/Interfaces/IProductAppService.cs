using System.Linq.Expressions;
using XStore.Application.ViewModel;
using XStore.Domain.Entities;

namespace XStore.Application.Interfaces
{
    public interface IProductAppService
    {
        Task<ProductViewModel> AddAsync(ProductViewModel product);
        ProductViewModel Update(ProductViewModel product);

        void Remove(Guid id);
        void Remove(Expression<Func<Product, bool>> expression);

        ProductViewModel GetById(Guid id);
        Task<ProductViewModel> GetByIdAsync(Guid id);

        IEnumerable<ProductViewModel> Search(Expression<Func<Product, bool>> predicate);
        Task<IEnumerable<ProductViewModel>> SearchAsync(Expression<Func<Product, bool>> predicate);
        IEnumerable<ProductViewModel> Search(Expression<Func<Product, bool>> predicate,
            int pageNumber,
            int pageSize);
    }
}
