using Microsoft.EntityFrameworkCore;
using Shop.Entities;

namespace Shop.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<ProductInBasket> ProductInBaskets { get; set; }
        public DbSet<User> Users { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(o =>
            {
                o.HasKey(e => e.Id);
                o.Property(e => e.Name).IsRequired();
                o.Property(e => e.Price).IsRequired();
            });

            modelBuilder.Entity<Basket>(o =>
            {
                o.HasKey(e => e.Id);
                o.Property(e => e.UserId).IsRequired();
                o.HasMany(e => e.ProductInBaskets)
                    .WithOne(e => e.Basket)
                    .HasForeignKey(p => p.BasketId);
            });

            modelBuilder.Entity<ProductInBasket>(o =>
            {
                o.HasKey(e => e.Id);
                o.HasOne(e => e.Product)
                    .WithMany(e => e.ProductInBaskets)
                    .HasForeignKey(e => e.ProductId);
            });

            modelBuilder.Entity<User>(o =>
            {
                o.HasKey(e => e.Id);
                o.Property(e => e.Username).IsRequired().HasMaxLength(100);
                o.Property(e => e.PasswordHash).IsRequired();
            });
        }
    }
}
