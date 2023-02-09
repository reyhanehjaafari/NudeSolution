using NudeSolution.Entities;

namespace NudeSolution.Services
{
    public interface ICategoryItemService
    {
        void Add(CategoryItemEntity categoryItem);
        List<CategoryItemEntity> GetAll();  
        void Delete(int categoryItem);
    }
}
