using Shop.Entities;
using Shop.Services.Models;

namespace Shop.Services
{
    public interface IProductService
    {
        int Add(CreateProduct product);
        Product Get(int id);
        void Update(Product product);
        void Delete(int id);
        Task<List<Product>> GetListAsync(int page, int itemsPerPage);
    }
}
