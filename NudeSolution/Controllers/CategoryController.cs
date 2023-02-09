using Microsoft.AspNetCore.Mvc;
using NudeSolution.DataAccess;
using NudeSolution.Entities;
using NudeSolution.Models;
using System.Text.Json;

namespace NudeSolution.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly NudeContext _dbContext;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(NudeContext dbContext, ILogger<CategoryController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("getAll")]
        public ActionResult<IEnumerable<CategoryEntity>> GetAll()
        {
            var categories = (from query in _dbContext.Categories
                              select new CategoryEntity
                              {
                                  CategoryId = query.CategoryId,
                                  Name = query.Name,
                                  CategoryItems = query.CategoryItems.ToList(),
                              }).ToList();

            var result = new { categories, categoriesTotalValue = categories.Sum(x => x.TotalValue) };

            _logger.Log(LogLevel.Information, $"Category fetched successfully. {JsonSerializer.Serialize(result)}");

            return Ok(result);
        }

        [HttpGet("seed")]
        public ActionResult<HttpContext> Seed()
        {

            var categories = _dbContext.Categories;
            List<CategoryEntity> categoryList = SetCategory();
            categories.AddRange(categoryList);
            _dbContext.SaveChanges();

            return Ok();
        }

        private List<CategoryEntity> SetCategory()
        {
            return new List<CategoryEntity> {
                new CategoryEntity{
                 Name="Electronics",
                    CategoryItems= new List<CategoryItemEntity>{
                new CategoryItemEntity{  Name="TV", Value=2000},
                new CategoryItemEntity{  Name="Playstation", Value=400},
                new CategoryItemEntity{  Name="stereo", Value=1600},
                                                                     },
                                  },
                new CategoryEntity{
                 Name="Clothing", CategoryItems =new List<CategoryItemEntity>{
                new CategoryItemEntity{ Name="Shirts", Value=1100 },
                new CategoryItemEntity{ Name="Jeans", Value=1100 }
                                                                 }
                                  },
                new CategoryEntity{
                Name="Kitchen",CategoryItems=new List<CategoryItemEntity>{
                new CategoryItemEntity{ Name="Pots and Pans", Value=3000},
                new CategoryItemEntity{ Name="Flatware", Value=500},
                new CategoryItemEntity{ Name="Knife Set", Value=500},
                new CategoryItemEntity{ Name="Misc", Value=1000},
                                                              }
                                  },
            };
        }


        [HttpGet("getCategories")]
        public List<CategoryViewModel> GetCategories()
        {
            return _dbContext.Categories.Select(x => new CategoryViewModel { Name = x.Name, CategoryId = x.CategoryId }).ToList();
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

                _dbContext.Add(newCategory);
                _dbContext.SaveChanges();
                _logger.Log(LogLevel.Information, "Category Item has been added successfully!");

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
                var categoryItem = _dbContext.CategoryItems.Find(categoryItemId);
                if (categoryItem != null)
                {
                    _dbContext.Remove(categoryItem);
                    _dbContext.SaveChanges();
                    _logger.Log(LogLevel.Information, "Category Item has been deleted successfully!");

                }
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
