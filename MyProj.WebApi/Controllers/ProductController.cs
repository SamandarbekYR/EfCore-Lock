using Microsoft.AspNetCore.Mvc;
using MyProj.WebApi.DTOs;
using MyProj.WebApi.Interfaces;
namespace MyProj.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _service;

        public ProductController(IProductRepository service)
        {
            this._service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddProductDto product)
        {
            var result = await _service.AddProductAsync(product);

            if (result)
                return Ok();

            return BadRequest("Bu Product allaqachon ro'yxatdan o'tkazilgan");
        }
    }
}
