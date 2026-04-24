namespace Shop.Repositories
{
    public interface IBasketRepository
    {
        int Add(int userId, int productId, int count);
        int? Remove(int userId, int productId, int count);
        int? RemoveAll(int userId, int productId);
    }
}
