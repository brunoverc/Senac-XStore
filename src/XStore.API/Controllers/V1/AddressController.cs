using Microsoft.AspNetCore.Mvc;
using XStore.Application.Interfaces;
using XStore.Application.ViewModel;

namespace XStore.API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/address")]
    public class AddressController : Controller
    {
        private readonly IAddressAppService _addressAppService;

        public AddressController(IAddressAppService addressAppService)
        {
            _addressAppService = addressAppService;
        }

        [HttpGet]
        // Traz todos os endereços cadastrados no sistema
        public async Task<ActionResult> Get()
        {
            var result = await _addressAppService.SearchAsync(a => true);

            return Ok(result);
        }

        [HttpGet("{id}")]
        // Traz um Endereço de acordo com o seu ID
        public async Task<ActionResult<AddressViewModel>> Get(Guid id)
        {
            var result = await _addressAppService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        //Insere um endereço no banco de dados
        public async Task<ActionResult> PostAsync([FromBody] AddressViewModel model)
        {
            var result = await _addressAppService.AddAsync(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] AddressViewModel model)
        {
            return Ok(_addressAppService.Update(model));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _addressAppService.Remove(id);
            return Ok();
        }
    }
}
