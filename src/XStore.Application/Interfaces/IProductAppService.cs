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

        /// <summary>
        /// Diminuir estoue
        /// </summary>
        /// <param name="productId">Id do produto</param>
        /// <param name="quantity">Quantidade</param>
        void DecreaseStock(Guid productId, int quantity);

        /// <summary>
        /// Checa a quantidade de itens no estoque
        /// </summary>
        /// <param name="productId">Id do Produto</param>
        /// <returns></returns>
        int CheckQuantityStock(Guid productId);  
    }
}
