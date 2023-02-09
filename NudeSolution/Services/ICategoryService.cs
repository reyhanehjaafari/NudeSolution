using NudeSolution.Entities;

namespace NudeSolution.Services
{
    public interface ICategoryService
    {
        void Add(CategoryEntity category);
        (List<CategoryEntity>, decimal?) GetAll();
        List<CategoryEntity> GetCategories();
        void Seed();
    }
}
