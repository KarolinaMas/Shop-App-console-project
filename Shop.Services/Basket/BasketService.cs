using Shop.Entities;
using Shop.Repositories;

namespace Shop.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository basketRepository;

        private readonly IProductRepository productRepository;

        public BasketService(
            IBasketRepository basketRepository,
            IProductRepository productRepository
        )
        {
            this.basketRepository = basketRepository;
            this.productRepository = productRepository;
        }

        // helper methods:
        private static void ValidateUserId(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid userId");
        }

        private void ValidateProductExists(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("Invalid productId");

            var product = productRepository.GetAsync(productId);

            if (product == null)
                throw new KeyNotFoundException("Product not found");
        }

        private static void ValidateCount(int count)
        {
            if (count <= 0)
                throw new ArgumentException("Count must be positive");
        }

        // main methods:
        public int Add(int userId, int productId, int count)
        {
            ValidateUserId(userId);

            ValidateProductExists(productId);

            ValidateCount(count);

            return basketRepository.Add(userId, productId, count);
        }

        public int? Remove(int userId, int productId, int count)
        {
            ValidateUserId(userId);

            ValidateProductExists(productId);

            ValidateCount(count);

            return basketRepository.Remove(userId, productId, count);
        }

        public int? RemoveAll(int userId, int productId)
        {
            ValidateUserId(userId);

            ValidateProductExists(productId);

            return basketRepository.RemoveAll(userId, productId);
        }

        public Basket Get(int userId)
        {
            ValidateUserId(userId);

            return basketRepository.Get(userId)
                ?? throw new KeyNotFoundException("Basket is not found");
        }
    }
}
