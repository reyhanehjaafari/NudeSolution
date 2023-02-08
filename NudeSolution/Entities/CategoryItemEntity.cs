using System.ComponentModel.DataAnnotations.Schema;

namespace NudeSolution.Entities
{
    public class CategoryItemEntity
    {
        public int CategoryItemId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }

        [ForeignKey("CategoryEntity")]
        public int CategoryId { get; set; }
        public CategoryEntity CategoryEntity { get; set; }
    }
}
