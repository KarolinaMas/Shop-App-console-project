using Shop.Entities;
using Shop.Repositories;

namespace Shop.Services
{
    public class ProductService : IProductService
    {
        public IProductRepository projectRepository;

        public ProductService(IProductRepository productRepository)
        {
            projectRepository = productRepository;
        }

        public int Add(Product product)
        {
            return projectRepository.Add(product);
        }

        public Product Get(int id)
        {
            return projectRepository.Get(id);
        }
    }
}
