using Microsoft.Extensions.DependencyInjection;
using Shop.Entities;
using Shop.Repositories;
using Shop.Services;

namespace Shop.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var collection = new ServiceCollection();

            collection.AddScoped<IProductRepository, ProductRepository>();
            collection.AddScoped<IBasketRepository, BasketRepository>();
            collection.AddScoped<IProductService, ProductService>();
            collection.AddScoped<IBasketService, BasketService>();

            var serviceProvider = collection.BuildServiceProvider();

            var productService = serviceProvider.GetRequiredService<IProductService>();
            var basketService = serviceProvider.GetRequiredService<IBasketService>();

            productService.Add(new Product() { Name = "Book", Price = 12.99M });
            productService.Add(new Product() { Name = "Lamp", Price = 37.99M });

            var product = productService.Get(1);

            Console.WriteLine($"id: {product.Id}; name: {product.Name}");

            basketService.Add(1, 1, 3);
            basketService.Add(2, 2, 2);

            var basket1 = basketService.Get(1);
            var basket2 = basketService.Get(2);

            Console.WriteLine($"user id: {basket1.UserId}; basket id: {basket1.Id};");
            Console.WriteLine($"user id: {basket2.UserId}; basket id: {basket2.Id};");
        }
    }
}
