using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Entities;

namespace Shop.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ShopDbContext dbContext;

        public BasketRepository(ShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(int userId, int productId, int count)
        {
            var basket = dbContext
                .Baskets.Include(b => b.ProductInBaskets)
                .FirstOrDefault(b => b.UserId == userId);

            if (basket == null)
            {
                basket = new Basket
                {
                    UserId = userId,
                    ProductInBaskets = new List<ProductInBasket>(),
                };

                dbContext.Baskets.Add(basket);
            }

            var existingProduct = basket.ProductInBaskets.FirstOrDefault(p =>
                p.ProductId == productId
            );

            if (existingProduct != null)
            {
                existingProduct.Count += count;
            }
            else
            {
                basket.ProductInBaskets.Add(
                    new ProductInBasket { ProductId = productId, Count = count }
                );
            }

            dbContext.SaveChanges();

            return basket.Id;
        }

        public int? Remove(int userId, int productId, int count)
        {
            {
                var basket = dbContext
                    .Baskets.Include(b => b.ProductInBaskets)
                    .FirstOrDefault(b => b.UserId == userId);

                if (basket == null)
                    return null;

                var product = basket.ProductInBaskets.FirstOrDefault(p => p.ProductId == productId);

                if (product == null)
                    return null;

                if (count >= product.Count)
                {
                    dbContext.ProductInBaskets.Remove(product);
                }
                else
                {
                    product.Count -= count;
                }

                dbContext.SaveChanges();

                return basket.Id;
            }
        }

        public int? RemoveAll(int userId, int productId)
        {
            var basket = dbContext
                .Baskets.Include(b => b.ProductInBaskets)
                .FirstOrDefault(b => b.UserId == userId);

            if (basket == null)
                return null;

            var product = basket.ProductInBaskets.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
                return null;

            dbContext.ProductInBaskets.Remove(product);

            dbContext.SaveChanges();

            return basket.Id;
        }

        public Basket Get(int userId)
        {
            return dbContext
                .Baskets.Include(b => b.ProductInBaskets)
                    .ThenInclude(p => p.Product)
                .FirstOrDefault(b => b.UserId == userId);
        }
    }
}
