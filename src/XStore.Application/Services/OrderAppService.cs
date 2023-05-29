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
        protected readonly IVoucherRepository _voucherRepository;
        protected readonly IMapper _mapper;

        public OrderAppService(IMapper mapper,
            IUnitOfWork unitOfWork,
            IMediator bus,
            IOrderRepository repository,
            IOrderItemRepository orderItemRepository,
            IAddressRepository addressRepository,
            IClientRepository clientRepository,
            IProductRepository productRepository,
            IVoucherRepository voucherRepository)
            : base(unitOfWork, bus)
        {
            _mapper = mapper;
            _orderRepository = repository;
            _orderItemRepository = orderItemRepository;
            _addressRepository = addressRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
            _voucherRepository = voucherRepository;
        }

        public async Task<IEnumerable<OrderItemViewModel>> DeleteItemInOrder(Guid orderItemId, Guid orderId)
        {
            //Delete no banco de um item da minha venda
            _orderItemRepository.Remove(orderItemId);
            Commit();
            //Pesquisa no banco novamente os itens que tem na minha venda pelo orderId
            IEnumerable<OrderItem> items = await  _orderItemRepository.SearchAsync(oi => oi.OrderId == orderId);

            var viewModels = _mapper.Map<IEnumerable<OrderItemViewModel>>(items);

            return viewModels;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<OrderSummaryViewModel> GetSummaryOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderViewModel> SetAddressDelivery(Guid orderId, AddressViewModel addressModel)
        {
            //Transforma meu viewModel de entrada em um objeto de domínio
            var domain = _mapper.Map<Address>(addressModel);

            //Cadastrar esse endereço no banco
            var address = await _addressRepository.AddAsync(entity: domain);
            Commit();

            //Alterar a minha order para o novo endereço
            var order = _orderRepository.GetById(orderId);
            order.SetAddress(address);

            _orderRepository.Update(order);
            Commit();
            //Retorno a venda em formato de ViewModel
            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<OrderViewModel> SetApplyVoucher(Guid orderId, string code)
        {
            //Recebo um código da promoção
            var vouchers = await _voucherRepository.SearchAsync(v => v.Code == code);

            //Esse código existe?
            if(!vouchers.Any())
            {
                throw new Exception("Não existe um cupom com o código informado");
            }

            var voucher = vouchers.FirstOrDefault();

            //O voucher está inativo
            if(!voucher.Active || voucher.ExpirationDate < DateTime.Now)
            {
                throw new Exception("A promoção informada não está mais ativa");
            }

            //O código já foi usado?
            if(voucher.Used.HasValue && voucher.Used.Value)
            {
                throw new Exception("Código de promoção já usado");
            }

            //1. Atribuindo o voucher a order
            //2. Calculando os descontos e valor total
            //3. fazendo um update
            var order = _orderRepository.GetById(orderId);
            order.SetVoucher(voucher);
            _orderRepository.Update(order);
            Commit();

            //Seto o meu voucher como usado 
            voucher.DebitAmount();

            //Retorno uma order para quem chamou
            _voucherRepository.Update(voucher);
            Commit();

            return _mapper.Map<OrderViewModel>(order);

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

            //Verificar se tem a quantidade em estoque
            if(model.Amount > product.StockQuantity)
            {
                throw new Exception("Quantidade de compra maior que a quantidade disponível");
            }

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
            //Consultando a minha orderItem
            var orderItem = _orderItemRepository.GetById(orderItemId);

            //Verificar se eu tenho estoque
            var product = _productRepository.GetById(orderItem.ProductId);
            if(newQuantity > product.StockQuantity)
            {
                throw new Exception("Quantidade de compra maior que a quantidade disponível");
            }

            //Fazer um update para a nova quantidade de itens
            orderItem.SetAmount(newQuantity);
            _orderItemRepository.Update(orderItem);
            Commit();
        }

        public async Task<OrderViewModel> UpdateStatusOrder(Guid orderId, OrderStatus newStatus)
        {
            //Pego a order
            var order = _orderRepository.GetById(orderId);

            //Quando meu novo status for:
            //Criado => Autorizado ou Em processamento
            //vai dar baixa em estoque
            if(order.OrderStatus == OrderStatus.Criado && 
                (newStatus == OrderStatus.Autorizado || newStatus == OrderStatus.EmProcessamento ))
            {
                //Baixa em estoque
                await SetStockOff(orderId);
            }


            //Quando ele for se tornar:
            //Em processamento => Cancelado ou Recusado
            //Autorizado => Recusado ou Cancelado
            //Vai retornar o item em estoque
            if (order.OrderStatus == OrderStatus.EmProcessamento &&
                (newStatus == OrderStatus.Recusado || newStatus == OrderStatus.Cancelado))
            {
                await SetReturnedStock(orderId);
            }

            //Setar o novo status da order
            order.SetStatus(newStatus);
            _orderRepository.Update(order);
            Commit();

            return _mapper.Map<OrderViewModel>(order);
        }

        private async Task SetReturnedStock(Guid orderId)
        {
            IEnumerable<OrderItem> items = await _orderItemRepository.SearchAsync(oi => oi.OrderId == orderId);
            foreach (OrderItem item in items)
            {
                var product = _productRepository.GetById(item.ProductId);
                product.SetStockQuantity(product.StockQuantity + item.Amount);
                _productRepository.Update(product);
            }

            Commit();
        }

        private async Task SetStockOff(Guid orderId)
        {
            //Baixa no estoque
            IEnumerable<OrderItem> items = await _orderItemRepository.SearchAsync(oi => oi.OrderId == orderId);

            foreach (OrderItem item in items)
            {
                var product = _productRepository.GetById(item.ProductId);

                //Garantir que eu ainda tenho isso em estoque
                if (item.Amount > product.StockQuantity)
                {
                    //Se eu não tenho o estoque eu vou parar o processamento e informar o usuário.
                    //Eu só concluo uma venda com todos os itens
                    throw new Exception($"Quantidade de compra maior que a quantidade disponível. Produto: {product.Name}");
                }
                //Altero a quantidade em estoque, faço o uptdate mas não comito no banco até verificar todos os itens
                product.SetStockQuantity(product.StockQuantity - item.Amount);
                _productRepository.Update(product);
            }
            //Agora posso comitar
            Commit();
        }
    }
}
