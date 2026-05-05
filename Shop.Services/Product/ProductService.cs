using Shop.Entities;
using Shop.Repositories;
using Shop.Services.Models;

namespace Shop.Services
{
    public class ProductService : IProductService
    {
        public const int DefaultItemsPerPage = 10;
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<int> AddAsync(CreateProduct createProduct)
        {
            if (string.IsNullOrWhiteSpace(createProduct.Name))
                throw new ArgumentException("Name is required");

            if (createProduct.Price <= 0)
                throw new ArgumentException("Price must be positive");

            if (createProduct.CountInStock < 0)
                throw new ArgumentException("Stock cannot be negative");

            var product = new Product
            {
                Name = createProduct.Name,
                Price = createProduct.Price,
                CountInStock = createProduct.CountInStock,
            };

            return await productRepository.AddAsync(product);
        }

        public async Task<Product?> GetAsync(int id)
        {
            return await productRepository.GetAsync(id);
        }

        public async Task UpdateAsync(CreateProduct createProduct)
        {
            if (string.IsNullOrWhiteSpace(createProduct.Name))
                throw new ArgumentException("Name is required");

            if (createProduct.Price <= 0)
                throw new ArgumentException("Price must be positive");

            if (createProduct.CountInStock < 0)
                throw new ArgumentException("Stock cannot be negative");

            var product = new Product
            {
                Name = createProduct.Name,
                Price = createProduct.Price,
                CountInStock = createProduct.CountInStock,
            };

            await productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await productRepository.DeleteAsync(id);
        }

        public async Task<List<Product>> GetListAsync(int page, int itemsPerPage)
        {
            if (page <= 0)
                page = 1;

            if (itemsPerPage <= 0)
                itemsPerPage = DefaultItemsPerPage;

            return await productRepository.GetListAsync(page, itemsPerPage);
        }
    }
}
