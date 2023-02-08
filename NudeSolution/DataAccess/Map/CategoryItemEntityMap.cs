using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NudeSolution.Entities;

namespace NudeSolution.DataAccess.Map
{
    public class CategoryItemEntityMap : IEntityTypeConfiguration<CategoryItemEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryItemEntity> builder)
        {
            builder.ToTable("CategoryItem");
            builder.Property(x => x.CategoryItemId).ValueGeneratedOnAdd();
            builder.HasKey(x => x.CategoryItemId);
        }
    }
}
