using Shop.Data;
using Shop.Entities;

namespace Shop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext dbContext;

        public ProductRepository(ShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(Product product)
        {
            var entityEntry = dbContext.Products.Add(product); // pridedam produkta i db ir isaugom i kintamaji entity entry

            dbContext.SaveChanges(); // tik iskvietus sita metoda informacija nueina i db

            return entityEntry.Entity.Id;
        }

        public Product Get(int id)
        {
            return dbContext.Products.SingleOrDefault(o => o.Id == id);
        }
    }
}
