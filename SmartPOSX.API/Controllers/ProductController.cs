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
        private readonly IImageService _imageService;
        public ProductController(IProductService productService, IImageService imageService)
        {
            _productService = productService;
            _imageService = imageService;
        }

        [HttpPost("images/upload")]
        public async Task<IActionResult> UploadImages([FromForm] List<IFormFile> files)
        {
            if (files == null || !files.Any())
            {
                return BadRequest(new { message = "No files provided." });
            }

            var results = new List<object>();
            foreach (var file in files)
            {
                var result = await _imageService.UploadImageAsync(file);
                if (result != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    results.Add(new { Url = result.SecureUrl.ToString(), PublicId = result.PublicId });
                }
            }
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Invalid product data." });
            }

            var response = await _productService.CreateProductAsync(request);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("delete-image/{publicId}")]
        public async Task<IActionResult> DeleteImage(string publicId)
        {
            try
            {
                var result = await _imageService.DeleteImageAsync(publicId);
                return Ok(new { message = "Image deleted", result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
