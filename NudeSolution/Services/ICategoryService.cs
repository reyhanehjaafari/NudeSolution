using NudeSolution.Entities;

namespace NudeSolution.Services
{
    public interface ICategoryService
    {
        (List<CategoryEntity>, decimal?) GetAll();
        List<CategoryEntity> GetCategories();
        void Seed();
    }
}
