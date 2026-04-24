using Shop.Entities;

namespace Shop.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private Dictionary<int, Basket> Baskets = [];

        public int Add(int userId, int productId, int count)
        {
            if (Baskets.TryGetValue(userId, out var basket)) // patikrinu ar krepselis egzistuoja
            {
                var existingProduct = basket.Products.FirstOrDefault(p => p.ProductId == productId); // patikrinu ar produktas jau yra krepselyje

                if (existingProduct != null)
                {
                    existingProduct.Count += count;
                    return basket.Id;
                }
                else
                {
                    basket.Products.Add(
                        new ProductInBasket
                        {
                            ProductId = productId,
                            BasketId = basket.Id,
                            Count = count,
                        }
                    );

                    return basket.Id;
                }
            }
            else
            {
                var maxId = Baskets.Keys.Any() ? Baskets.Keys.Max() : 0;
                var newId = maxId + 1;

                var newBasket = new Basket
                {
                    Id = newId,
                    UserId = userId,
                    Products = new()
                    {
                        new ProductInBasket()
                        {
                            ProductId = productId,
                            BasketId = newId,
                            Count = count,
                        },
                    },
                };

                Baskets.Add(userId, newBasket);

                return newBasket.Id;
            }
        }

        public int? Remove(int userId, int productId, int count)
        {
            if (!Baskets.TryGetValue(userId, out var basket))
            {
                return null;
            }

            var product = basket.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return null;
            }

            if (count >= product.Count)
            {
                basket.Products.Remove(product);
                return basket.Id;
            }

            product.Count -= count;

            return basket.Id;
        }

        public int? RemoveAll(int userId, int productId)
        {
            if (!Baskets.TryGetValue(userId, out var basket))
            {
                return null;
            }

            var product = basket.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return null;
            }

            basket.Products.Remove(product);

            return basket.Id;
        }
    }
}
