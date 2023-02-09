using NudeSolution.DataAccess;
using NudeSolution.Entities;

namespace NudeSolution.Services
{
    public class CategoryItemService : ICategoryItemService
    {
        private readonly NudeContext _dbContext;
        private readonly ILogger<CategoryItemService> _logger;

        public CategoryItemService(NudeContext dbContext, ILogger<CategoryItemService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Add(CategoryItemEntity categoryItem)
        {
            _dbContext.Add(categoryItem);
            _dbContext.SaveChanges();
            _logger.Log(LogLevel.Information, "Category Item has been added successfully!");
        }

        public List<CategoryItemEntity> GetAll()
        {
            return _dbContext.CategoryItems.ToList();
        }

        public void Delete(int categoryItemId)
        {
            var categoryItem = _dbContext.CategoryItems.Find(categoryItemId);
            if (categoryItem != null)
            {
                _dbContext.Remove(categoryItem);
                _dbContext.SaveChanges();
                _logger.Log(LogLevel.Information, "Category Item has been deleted successfully!");
            }
        }
    }
}
