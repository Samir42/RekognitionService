using Microsoft.AspNetCore.Mvc;
using RekognitionService.Application.Interfaces;

namespace RekognitionService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetCategories()
        {
            var categoriesFromDB = await _categoryService.GetAll();

            return Ok(categoriesFromDB);
        }
    }
}