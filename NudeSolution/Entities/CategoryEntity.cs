namespace NudeSolution.Entities
{
    public class CategoryEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryItemEntity> CategoryItems { get; set; }
        public decimal? TotalValue => CategoryItems?.Where(x => x.CategoryId == CategoryId).Sum(x => x.Value);
    }
}
