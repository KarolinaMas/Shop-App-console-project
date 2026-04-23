using Shop.Entities;
using Shop.Repositories;
using Shop.Services;

namespace Shop.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IProductRepository productRepository = new ProductRepository();

            IProductService productService = new ProductService(productRepository);

            productService.Add(new Product() { Name = "Book", Price = 12.99M });

            var product = productService.Get(1);

            Console.WriteLine($"id: {product.Id}; name: {product.Name}");
        }
    }
}
