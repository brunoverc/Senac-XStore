using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public virtual async Task<AddressViewModel>  AddAsync(AddressViewModel viewModel)
        {
            Address domain = _mapper.Map<Address>(viewModel);

            domain = await _repository.AddAsync(domain);
            Commit();

            AddressViewModel viewModelReturn = _mapper.Map<AddressViewModel>(domain);
            return viewModelReturn;
        }
    }
}
