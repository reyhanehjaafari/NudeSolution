using NudeSolution.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace NudeSolution.DAL
{
    public class NudeContext : DbContext
    {
        public NudeContext(DbContextOptions<NudeContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }

}