using Shop.Entities;
using Shop.Services.Models;

namespace Shop.Services
{
    public interface IProductService
    {
        Task<int> AddAsync(CreateProduct createProduct);
        Task<Product?> GetAsync(int id);
        Task UpdateAsync(int id, CreateProduct createProduct);
        Task DeleteAsync(int id);
        Task<List<Product>> GetListAsync(int page, int itemsPerPage);
    }
}
