using Microsoft.AspNetCore.Mvc;
using SmartPOSX.Core.DTOs.Products;
using SmartPOSX.Core.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartPOSX.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _productService.GetProductListAsync();

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _productService.GetProductByIdAsync(id);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _productService.CreateProductAsync(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
    }
}
