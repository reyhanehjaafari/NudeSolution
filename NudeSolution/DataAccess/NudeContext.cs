using NudeSolution.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace NudeSolution.DataAccess
{
    public class NudeContext : DbContext
    {
        public NudeContext(DbContextOptions<NudeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoryItemEntity> CategoryItems { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}