using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XStore.Application.Interfaces;
using XStore.Application.ViewModel;
using XStore.Core.Enums;
using XStore.Domain.Entities;
using XStore.Domain.Interfaces;
using XStore.Domain.Shared.Transaction;

namespace XStore.Application.Services
{
    public class OrderAppService : BaseService, IOrderAppService
    {
        protected readonly IOrderRepository _orderRepository;
        protected readonly IOrderItemRepository _orderItemRepository;
        protected readonly IAddressRepository _addressRepository;
        protected readonly IClientRepository _clientRepository;
        protected readonly IProductRepository _productRepository;
        protected readonly IMapper _mapper;

        public OrderAppService(IMapper mapper,
            IUnitOfWork unitOfWork,
            IMediator bus,
            IOrderRepository repository,
            IOrderItemRepository orderItemRepository,
            IAddressRepository addressRepository,
            IClientRepository clientRepository,
            IProductRepository productRepository)
            : base(unitOfWork, bus)
        {
            _mapper = mapper;
            _orderRepository = repository;
            _orderItemRepository = orderItemRepository;
            _addressRepository = addressRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }

        public Task<IEnumerable<OrderItemViewModel>> DeleteItemInOrder(Guid orderItemId, Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderSummaryViewModel> GetSummaryOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderViewModel> SetAddressDelivery(Guid orderId, AddressViewModel addressModel)
        {
            throw new NotImplementedException();
        }

        public Task<OrderViewModel> SetApplyVoucher(Guid orderId, VoucherViewModel voucherModel)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderViewModel> SetCreateNewOrder(OrderViewModel model)
        {
            model.OrderStatus = OrderStatus.Criado;
            var domain = _mapper.Map<Order>(model);
            domain.CalculateOrderValue();
            domain.SetCode();

            //Setar o endereço do cliente na order, caso ele queira alterar depois ele pode.
            //(Como eu só precisava o Address eu usei na mesma linha chamar o
            //Address do client que veio pelo getById)
            var addressClientId = _clientRepository.GetById(domain.ClientId).AddressId;
            var addressClient = _addressRepository.GetById(addressClientId);
            domain.SetAddress(addressClient);

            var order = await _orderRepository.AddAsync(domain);
            Commit();

            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<IEnumerable<OrderItemViewModel>> SetInsertNewItem(OrderItemViewModel model, Guid orderId)
        {
            var domain = _mapper.Map<OrderItem>(model);

            var product = _productRepository.GetById(model.ProductId);
            domain.SetProductName(product.Name);
            domain.SetProductImage(product.Image);


            //Use o _ quando quiser descartar algo.
            //O meu método AddAsync retorna um orderItem,
            //mas eu não tenho uso pra ele, então vou descartar
            _ = await _orderItemRepository.AddAsync(domain);
            Commit();

            //Vou buscar todos os itens da minha Order.
            var orderItems = await _orderItemRepository.SearchAsync(oi => oi.OrderId == orderId);

            //Conversão via Cast, ele vai converter o orderItems em IEnumerable<OrderItemViewMode>
            //Cuidado ao usar essa forma de conversão pois precisa ter certeza que ela é possível
            return _mapper.Map<IEnumerable<OrderItemViewModel>>(orderItems);
        }

        public void UpdateQuantityItemInOrder(Guid orderItemId, int newQuantity)
        {
            throw new NotImplementedException();
        }

        public Task<OrderViewModel> UpdateStatusOrder(Guid orderId, OrderStatus newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
