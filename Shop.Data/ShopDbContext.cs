using Microsoft.EntityFrameworkCore;
using Shop.Entities;

namespace Shop.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        // public DbSet<Basket> Baskets { get; set; }
        public DbSet<ProductInBasket> ProductInBaskets { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(o =>
            {
                o.HasKey(o => o.Id);
                o.Property(o => o.Name).IsRequired();
                o.Property(o => o.Price).IsRequired();
            });

            // modelBuilder.Entity<Basket>(o =>
            // {
            //     o.HasKey(o => o.Id);
            //     o.Property(o => o.UserId).IsRequired();
            //     o.HasMany(o => o.Products).WithOne().HasForeignKey(p => p.BasketId);
            // });

            modelBuilder.Entity<ProductInBasket>(o =>
            {
                o.HasKey(e => e.Id);
                o.HasOne(e => e.Product)
                    .WithMany(e => e.ProductInBaskets)
                    .HasForeignKey(e => e.ProductId);
            });
        }
    }
}
