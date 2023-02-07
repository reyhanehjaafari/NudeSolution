using NudeSolution.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NudeSolution.DAL.Map
{
    public class UserEntityMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
        }
    }
}
