using Shop.Entities;

namespace Shop.Repositories
{
    public interface IProductRepository
    {
        int Add(Product product);

        Product Get(int id);
        void Update(Product product);
        void Delete(int id);
        Task<List<Product>> GetListAsync(int page, int itemsPerPage);
    }
}
