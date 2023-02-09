using NudeSolution.DataAccess;
using NudeSolution.Entities;
using System.Text.Json;

namespace NudeSolution.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly NudeContext _dbContext;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(NudeContext dbContext, ILogger<CategoryService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Add(CategoryEntity category)
        {
            _dbContext.Add(category);
            _dbContext.SaveChanges();
            _logger.Log(LogLevel.Information, "Category has been added successfully!");
        }

        public (List<CategoryEntity>, decimal?) GetAll()
        {
            var categories = (from query in _dbContext.Categories
                              select new CategoryEntity
                              {
                                  CategoryId = query.CategoryId,
                                  Name = query.Name,
                                  CategoryItems = query.CategoryItems.ToList(),
                              }).ToList();

            var result = (categories, categories.Sum(x => x.TotalValue));

            _logger.Log(LogLevel.Information, "Category fetched successfully");

            return result;
        }

        public List<CategoryEntity> GetCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public void Seed()
        {
            var categories = _dbContext.Categories;
            List<CategoryEntity> categoryList = SetCategoryWithItems();
            categories.AddRange(categoryList);
            _dbContext.SaveChanges();
        }

        private List<CategoryEntity> SetCategoryWithItems()
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
