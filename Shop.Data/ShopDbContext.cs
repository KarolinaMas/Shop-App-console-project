using Microsoft.EntityFrameworkCore;
using Shop.Entities;

namespace Shop.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(o =>
            {
                o.HasKey(o => o.Id);
                o.Property(o => o.Name).IsRequired();
                o.Property(o => o.Price).IsRequired();
            });
        }
    }
}
