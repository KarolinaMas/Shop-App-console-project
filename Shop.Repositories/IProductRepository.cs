using Shop.Entities;

namespace Shop.Repositories
{
    public interface IProductRepository
    {
        int Add(Product product);

        Product Get(int id);
    }
}
