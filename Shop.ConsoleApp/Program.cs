using Shop.Entities;
using Shop.Services;

namespace Shop.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IProductService productService = new ProductService();

            productService.Add(new Product() { Name = "Book", Price = 12.99M });

            var product = productService.Get(1);

            Console.WriteLine($"id: {product.Id}; name: {product.Name}");
        }
    }
}
