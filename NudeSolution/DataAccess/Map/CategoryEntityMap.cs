using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NudeSolution.Entities;

namespace NudeSolution.DataAccess.Map
{
    public class CategoryEntityMap : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("Category");
            builder.Property(x => x.CategoryId).ValueGeneratedOnAdd();
            builder.HasKey(x => x.CategoryId);
        }
    }
}
