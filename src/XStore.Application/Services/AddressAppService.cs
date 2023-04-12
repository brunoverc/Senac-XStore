using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using XStore.Application.Interfaces;
using XStore.Application.ViewModel;
using XStore.Domain.Entities;
using XStore.Domain.Interfaces;
using XStore.Domain.Shared.Transaction;

namespace XStore.Application.Services
{
    public class AddressAppService : BaseService, IAddressAppService
    {
        protected readonly IAddressRepository _repository;
        protected readonly IMapper _mapper;

        public AddressAppService(IAddressRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMediator bus)
            : base(unitOfWork, bus)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AddressViewModel> AddAsync(AddressViewModel viewModel)
        {
            Address domain = _mapper.Map<Address>(viewModel);
            domain = await _repository.AddAsync(domain);
            Commit();

            AddressViewModel viewModelReturn = _mapper.Map<AddressViewModel>(domain);
            return viewModelReturn;
        }

        public AddressViewModel Update(AddressViewModel viewModel)
        {
            var domain = _mapper.Map<Address>(viewModel);
            domain = _repository.Update(domain);
            Commit();
            AddressViewModel viewModelReturn = _mapper.Map<AddressViewModel>(domain);
            return viewModelReturn;
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
            Commit();
        }

        public void Remove(Expression<Func<Address, bool>> expression)
        {
            _repository.Remove(expression);
            Commit();
        }

        public AddressViewModel GetById(Guid id)
        {
            var domain = _repository.GetById(id);
            var viewModel = _mapper.Map<AddressViewModel>(domain);
            return viewModel;
        }

        public async Task<AddressViewModel> GetByIdAsync(Guid id)
        {
            var domain = await _repository.GetByIdAsync(id);
            var viewModel = _mapper.Map<AddressViewModel>(domain);
            return viewModel;
        }

        public IEnumerable<AddressViewModel> Search(Expression<Func<Address, bool>> predicate)
        {
            var domain = _repository.Search(predicate);
            var viewModels = _mapper.Map<IEnumerable<AddressViewModel>>(domain);
            return viewModels;
        }

        public Task<IEnumerable<AddressViewModel>> SearchAsync(Expression<Func<Address, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AddressViewModel> Search(Expression<Func<Address, bool>> predicate, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
