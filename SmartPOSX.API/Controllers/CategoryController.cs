using Microsoft.AspNetCore.Mvc;
using SmartPOSX.Core.DTOs.Categories;
using SmartPOSX.Core.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartPOSX.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _categoryService.GetAllCategoriesAsync();
            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);

            return response.Success ? Ok(response) : NotFound(response.Message);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _categoryService.CreateCategoryAsync(request);

            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
