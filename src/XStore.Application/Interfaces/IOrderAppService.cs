using XStore.Application.ViewModel;
using XStore.Core.Enums;

namespace XStore.Application.Interfaces
{
    public interface IOrderAppService
    {
        /// <summary>
        /// Método usado para criar uma nova venda
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Uma order</returns>
        Task<OrderViewModel> SetCreateNewOrder(OrderViewModel model);
        /// <summary>
        /// Insere um novo item na venda
        /// </summary>
        /// <param name="model">Order Item</param>
        /// <param name="orderId">Id da Order</param>
        /// <returns>Retorna a lista de itens da order</returns>
        Task<IEnumerable<OrderItemViewModel>> SetInsertNewItem(OrderItemViewModel model, Guid orderId);
        /// <summary>
        /// Deleta um item da order
        /// </summary>
        /// <param name="orderItemId">OrderItem Id</param>
        /// <param name="orderId">Order Id</param>
        /// <returns>Retorna a lista de itens da order</returns>
        Task<IEnumerable<OrderItemViewModel>> DeleteItemInOrder(Guid orderItemId, Guid orderId);
        /// <summary>
        /// Altera a quantidade de itens na order
        /// </summary>
        /// <param name="orderItemId">Order Item Id</param>
        /// <param name="newQuantity">Nova quantidade</param>
        void UpdateQuantityItemInOrder(Guid orderItemId, int newQuantity);
        /// <summary>
        /// Altera o status da order
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <param name="newStatus">Novo Status</param>
        /// <returns></returns>
        Task<OrderViewModel> UpdateStatusOrder(Guid orderId, OrderStatus newStatus);
        /// <summary>
        /// Seta o endereço de entrega
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <param name="addressModel">Address Model</param>
        /// <returns></returns>
        Task<OrderViewModel> SetAddressDelivery(Guid orderId, AddressViewModel addressModel);
        /// <summary>
        /// Aplicar um voucher a venda
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <param name="voucherModel">Voucher View Model</param>
        /// <returns>Order View Model</returns>
        Task<OrderViewModel> SetApplyVoucher(Guid orderId, VoucherViewModel voucherModel);
        /// <summary>
        /// Traz um resumo da venda
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <returns>Objeto Order Summary View Model</returns>
        Task<OrderSummaryViewModel> GetSummaryOrder(Guid orderId);

        //TO DO => Pagar venda



    }
}
