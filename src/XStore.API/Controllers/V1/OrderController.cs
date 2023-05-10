using Microsoft.AspNetCore.Mvc;
using XStore.Application.Interfaces;
using XStore.Application.Services;
using XStore.Application.ViewModel;

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

        [HttpPost("CreateNewOrder")]
        public async Task<ActionResult> CreateNewOrderAsync([FromBody] OrderViewModel model)
        {
            var result = await _orderAppService.SetCreateNewOrder(model);
            return Ok(result);
        }

        [HttpPost("InsertNewItem")]
        public async Task<IEnumerable<OrderItemViewModel>> InsertNewItem([FromBody] OrderItemViewModel model, 
            Guid orderId)
        {
            var result = await _orderAppService.SetInsertNewItem(model, orderId);
            return result;
        }
    }
}
