using Microsoft.AspNetCore.Mvc;
using NudeSolution.DataAccess;
using NudeSolution.Entities;
using System.Data.Entity;

namespace NudeSolution.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly NudeContext _dbContext;
        public CategoryController(NudeContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getAll")]
        public ActionResult<IEnumerable<CategoryEntity>> GetCategories()
        {
            var categories = (from query in _dbContext.Categories
                              select new CategoryEntity
                              {
                                  CategoryId = query.CategoryId,
                                  Name = query.Name,
                                  CategoryItems = query.CategoryItems.ToList(),
                              }).ToList();

            return Ok(new { categories, categoriesTotalValue = categories.Sum(x => x.TotalValue) });
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
    }
}
