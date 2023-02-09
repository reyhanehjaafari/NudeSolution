using Microsoft.AspNetCore.Mvc;
using NudeSolution.Entities;
using NudeSolution.Models;
using NudeSolution.Services;

namespace NudeSolution.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryItemService _categoryItemService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ICategoryItemService categoryItemService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _categoryItemService = categoryItemService;
            _logger = logger;
        }

        [HttpGet("getAll")]
        public ActionResult<IEnumerable<CategoryEntity>> GetAll()
        {
            var result = _categoryService.GetAll();
            return Ok(new { categories = result.Item1, totalValue = result.Item2 });
        }

        [HttpGet("seed")]
        public ActionResult<HttpContext> Seed()
        {
            _categoryService.Seed();
            return Ok();
        }

        [HttpGet("getCategories")]
        public IEnumerable<CategoryViewModel> GetCategories()
        {
            return _categoryService.GetCategories().Select(x => new CategoryViewModel
            {
                Name = x.Name,
                CategoryId = x.CategoryId
            });
        }

        [HttpPost("CreateCategoryItem")]
        public IActionResult CreateCategoryItem([FromBody] CategoryItemViewModel categoryItemViewModel)
        {
            try
            {
                var newCategory = new CategoryItemEntity
                {
                    CategoryId = categoryItemViewModel.CategoryId,
                    Name = categoryItemViewModel.Name,
                    Value = categoryItemViewModel.Value
                };

                _categoryItemService.Add(newCategory);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return BadRequest();
            };

            return Ok();
        }

        [HttpDelete("deleteCategoryItem")]
        public IActionResult deleteCategoryItem([FromQuery] int categoryItemId)
        {
            try
            {
                _categoryItemService.Delete(categoryItemId);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);

                return BadRequest();
            }
            return Ok();
        }

    }
}
