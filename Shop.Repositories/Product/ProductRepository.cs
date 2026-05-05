using Microsoft.EntityFrameworkCore;
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

        public async Task<int> AddAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);

            await dbContext.SaveChangesAsync(); // tik iskvietus sita metoda informacija nueina i db

            return product.Id;
        }

        public async Task<Product> GetAsync(int id)
        {
            return await dbContext.Products.SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
            var existing = await dbContext.Products.FindAsync(product.Id);

            if (existing == null)
                return;

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.CountInStock = product.CountInStock;

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await dbContext.Products.Where(p => p.Id == id).ExecuteDeleteAsync();

            // await dbContext.SaveChangesAsync(); ExecuteDeleteAsync() jau issaugo
        }

        public async Task<List<Product>> GetListAsync(int page, int itemsPerPage) // paging pvz.
        {
            return await dbContext
                .Products.OrderBy(p => p.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }
    }
}
