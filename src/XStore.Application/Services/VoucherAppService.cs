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
    public class VoucherAppService : BaseService, IVoucherAppService
    {
        protected readonly IVoucherRepository _repository;
        protected readonly IMapper _mapper;

        public VoucherAppService(IVoucherRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMediator bus)
            : base(unitOfWork, bus)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<VoucherViewModel> AddAsync(VoucherViewModel viewModel)
        {
            Voucher domain = _mapper.Map<Voucher>(viewModel);
            domain = await _repository.AddAsync(domain);
            Commit();

            VoucherViewModel viewModelReturn = _mapper.Map<VoucherViewModel>(domain);
            return viewModelReturn;
        }

        public VoucherViewModel Update(VoucherViewModel viewModel)
        {
            var domain = _mapper.Map<Voucher>(viewModel);
            domain = _repository.Update(domain);
            Commit();
            VoucherViewModel viewModelReturn = _mapper.Map<VoucherViewModel>(domain);
            return viewModelReturn;
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
            Commit();
        }

        public void Remove(Expression<Func<Voucher, bool>> expression)
        {
            _repository.Remove(expression);
            Commit();
        }

        public VoucherViewModel GetById(Guid id)
        {
            var domain = _repository.GetById(id);
            var viewModel = _mapper.Map<VoucherViewModel>(domain);
            return viewModel;
        }

        public async Task<VoucherViewModel> GetByIdAsync(Guid id)
        {
            var domain = await _repository.GetByIdAsync(id);
            var viewModel = _mapper.Map<VoucherViewModel>(domain);
            return viewModel;
        }

        public IEnumerable<VoucherViewModel> Search(Expression<Func<Voucher, bool>> predicate)
        {
            var domain = _repository.Search(predicate);
            var viewModels = _mapper.Map<IEnumerable<VoucherViewModel>>(domain);
            return viewModels;
        }

        public async Task<IEnumerable<VoucherViewModel>> SearchAsync(Expression<Func<Voucher, bool>> predicate)
        {
            var domain = await _repository.SearchAsync(predicate);
            var viewModels = _mapper.Map<IEnumerable<VoucherViewModel>>(domain);
            return viewModels;
        }

        public IEnumerable<VoucherViewModel> Search(Expression<Func<Voucher, bool>> predicate, int pageNumber, int pageSize)
        {
            var domain = _repository.Search(predicate, pageNumber, pageSize);
            var viewModels = _mapper.Map<IEnumerable<VoucherViewModel>>(domain);
            return viewModels;
        }
    }
}
