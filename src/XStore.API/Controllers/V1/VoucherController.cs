using Microsoft.AspNetCore.Mvc;
using XStore.Application.Interfaces;
using XStore.Application.ViewModel;

namespace XStore.API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/Voucher")]
    public class VoucherController : Controller
    {
        private readonly IVoucherAppService _voucherAppService;

        public VoucherController(IVoucherAppService voucherAppService)
        {
            _voucherAppService = voucherAppService;
        }

        [HttpGet]
        // Traz todos os vouchers cadastrados no sistema
        public async Task<ActionResult> Get()
        {
            var result = await _voucherAppService.SearchAsync(a => true);

            return Ok(result);
        }

        [HttpGet("{id}")]
        // Traz um Voucher de acordo com o seu ID
        public async Task<ActionResult<VoucherViewModel>> Get(Guid id)
        {
            var result = await _voucherAppService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        //Insere um voucher no banco de dados
        public async Task<ActionResult> PostAsync([FromBody] VoucherViewModel model)
        {
            var result = await _voucherAppService.AddAsync(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] VoucherViewModel model)
        {
            return Ok(_voucherAppService.Update(model));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _voucherAppService.Remove(id);
            return Ok();
        }
    }
}
