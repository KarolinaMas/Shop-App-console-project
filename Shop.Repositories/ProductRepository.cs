using Shop.Entities;

namespace Shop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private Dictionary<int, Product> Products { get; set; } = [];

        public int Add(Product product)
        {
            var maxId = Products.Keys.Any() ? Products.Keys.Max() : 0;
            product.Id = maxId + 1;

            Products.Add(product.Id, product);

            return product.Id;
        }

        public Product Get(int id)
        {
            if (Products.TryGetValue(id, out var product))
                return product;

            return null;
        }
    }
}
