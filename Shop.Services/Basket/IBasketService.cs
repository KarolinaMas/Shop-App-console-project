using Shop.Entities;

namespace Shop.Services
{
    public interface IBasketService
    {
        int Add(int userId, int productId, int count);
        int? Remove(int userId, int productId, int count);
        int? RemoveAll(int userId, int productId);
        Basket Get(int userId);
    }
}
