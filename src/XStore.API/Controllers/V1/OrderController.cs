using Microsoft.AspNetCore.Mvc;
using XStore.Application.Interfaces;
using XStore.Application.Services;
using XStore.Application.ViewModel;
using XStore.Core.Enums;

namespace XStore.API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/order")]
    public class OrderController : Controller
    {
        protected readonly IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        [HttpPost("createNewOrder")]
        public async Task<ActionResult> CreateNewOrderAsync([FromBody] OrderViewModel model)
        {
            var result = await _orderAppService.SetCreateNewOrder(model);
            return Ok(result);
        }

        [HttpPost("insertNewItem")]
        public async Task<IEnumerable<OrderItemViewModel>> InsertNewItem([FromBody] OrderItemViewModel model, 
            Guid orderId)
        {
            var result = await _orderAppService.SetInsertNewItem(model, orderId);
            return result;
        }

        [HttpDelete("{orderItemId}/{orderId}")]
        public async Task<IEnumerable<OrderItemViewModel>> DeleteItem(Guid orderItemId, Guid orderId)
        {
            var result = await _orderAppService.DeleteItemInOrder(orderItemId, orderId);
            return result;
        }

        [HttpPut("addressDelivery/{orderId}")]
        public async Task<OrderViewModel> SetAddressDelivery(Guid orderId, [FromBody] AddressViewModel addressModel)
        {
            var result = await _orderAppService.SetAddressDelivery(orderId, addressModel);
            return result;
        }

        [HttpPut("quantityItem/{orderItemId}/{newQuantity}")]
        public bool UpdateQuantityItem(Guid orderItemId, int newQuantity)
        {
            _orderAppService.UpdateQuantityItemInOrder(orderItemId, newQuantity);
            return true;
        }

        [HttpPut("updateStatusOrder/{orderId}/{newStatus}")]
        public async Task<OrderViewModel> UpdateStatus(Guid orderId, OrderStatus newStatus)
        {
            var result = await _orderAppService.UpdateStatusOrder(orderId, newStatus);
            return result;
        }
    }
}
