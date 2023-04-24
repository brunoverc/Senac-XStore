using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XStore.Application.Interfaces;
using XStore.Application.ViewModel;
using XStore.Domain.Entities;
using XStore.Domain.Interfaces;
using XStore.Domain.Shared.Transaction;

namespace XStore.Application.Services
{
    public class ProductAppService : BaseService, IProductAppService
    {
        protected readonly IProductRepository _repository;
        protected readonly IMapper _mapper;

        public ProductAppService(IProductRepository repository, 
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMediator bus) : base(unitOfWork, bus)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductViewModel> AddAsync(ProductViewModel viewModel)
        {
            Product domain = _mapper.Map<Product>(viewModel);
            domain = await _repository.AddAsync(domain);
            Commit();

            ProductViewModel viewModelReturn = _mapper.Map<ProductViewModel>(domain);
            return viewModelReturn;
        }

        public ProductViewModel GetById(Guid id)
        {
            var domain = _repository.GetById(id);
            var viewModel = _mapper.Map<ProductViewModel>(domain);
            return viewModel;
        }

        public async Task<ProductViewModel> GetByIdAsync(Guid id)
        {
            var domain = await _repository.GetByIdAsync(id);
            var viewModel = _mapper.Map<ProductViewModel>(domain);
            return viewModel;
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
            Commit();
        }

        public void Remove(Expression<Func<Product, bool>> expression)
        {
            _repository.Remove(expression);
            Commit();
        }

        public IEnumerable<ProductViewModel> Search(Expression<Func<Product, bool>> predicate)
        {
            var domain = _repository.Search(predicate);
            var viewModels = _mapper.Map<IEnumerable<ProductViewModel>>(domain);
            return viewModels;
        }

        public async Task<IEnumerable<ProductViewModel>> SearchAsync(Expression<Func<Product, bool>> predicate)
        {
            var domain = await _repository.SearchAsync(predicate);
            var viewModels = _mapper.Map<IEnumerable<ProductViewModel>>(domain);
            return viewModels;
        }

        public IEnumerable<ProductViewModel> Search(Expression<Func<Product, 
            bool>> predicate, 
            int pageNumber, 
            int pageSize)
        {
            var domain = _repository.Search(predicate, pageNumber, pageSize);
            var viewModels = _mapper.Map<IEnumerable<ProductViewModel>>(domain);
            return viewModels;
        }

        public ProductViewModel Update(ProductViewModel viewModel)
        {
            var domain = _mapper.Map<Product>(viewModel);
            domain = _repository.Update(domain);
            Commit();
            ProductViewModel viewModelReturn = _mapper.Map<ProductViewModel>(domain);
            return viewModelReturn;
        }

        public void DecreaseStock(Guid productId, int quantity)
        {
            //Trouxe meu produto pelo id dele
            var domain = _repository.GetById(productId);

            var quantityStock = CheckQuantityStock(productId);

            if(!domain.Active)
            {
                throw new Exception("Ops! Esse item não está mais ativo.");
            }

            if(quantity > quantityStock)
            {
                throw new Exception("Não temos mais disponível essa quantidade em estoque.");
            }

            //Vou retirar a quantidade em estoque
            domain.SetStockQuantity(domain.StockQuantity - quantity);
            //Atualizo meu produto
            domain = _repository.Update(domain);

            Commit();
        }

        public int CheckQuantityStock(Guid productId)
        {
            //Busco o meu produto
            var domain = _repository.GetById(productId);
            var quantity = domain.StockQuantity;
            return quantity;
        }
    }
}
