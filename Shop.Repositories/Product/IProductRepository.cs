using Shop.Entities;

namespace Shop.Repositories
{
    public interface IProductRepository
    {
        Task<int> AddAsync(Product product);
        Task<Product?> GetAsync(int id);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<List<Product>> GetListAsync(int page, int itemsPerPage);
    }
}
