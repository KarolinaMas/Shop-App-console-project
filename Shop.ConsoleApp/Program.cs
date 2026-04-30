using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Data;
using Shop.Entities;
using Shop.Repositories;
using Shop.Services;

namespace Shop.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = BuildHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var dbContext = serviceProvider.GetRequiredService<ShopDbContext>();

                dbContext.Database.EnsureDeleted(); // visada istrina db, nesikaupia nereikalingi duomenys testavimo metu
                dbContext.Database.Migrate();

                var productService = serviceProvider.GetRequiredService<IProductService>();
                var basketService = serviceProvider.GetRequiredService<IBasketService>();
                var userService = serviceProvider.GetRequiredService<IUserService>();

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

                var user1 = userService.Register("karole", "karole123", "karole123");
                var user2 = userService.Register("john", "john123", "john123");
            }
        }

        public static IHost BuildHost(string[] args) // args naudojant pavyksta pasiekti appsettings.json
        {
            var host = Host.CreateDefaultBuilder(args)
                .UseEnvironment("Development")
                .ConfigureServices(
                    (context, services) =>
                    {
                        var connectionString = context.Configuration.GetConnectionString(
                            "DefaultConnection"
                        );

                        services.AddDbContext<ShopDbContext>(options =>
                        {
                            options.UseMySql(
                                connectionString,
                                ServerVersion.AutoDetect(connectionString)
                            );
                        });

                        services.AddScoped<IProductRepository, ProductRepository>();
                        services.AddScoped<IBasketRepository, BasketRepository>();
                        services.AddScoped<IProductService, ProductService>();
                        services.AddScoped<IBasketService, BasketService>();
                        services.AddScoped<IUserRepository, UserRepository>();
                        services.AddScoped<IUserService, UserService>();
                    }
                );

            return host.Build();
        }
    }
}
