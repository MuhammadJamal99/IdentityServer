using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoffeeShopController : ControllerBase
    {
        private readonly ICoffeeShopService _CoffeeShopService;
        public CoffeeShopController(ICoffeeShopService CoffeeShopService)
        {
            _CoffeeShopService = CoffeeShopService;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _CoffeeShopService.GetList();
            return Ok(result);
        } 
    }
}