using Shop.Entities;

namespace Shop.Repositories
{
    public interface IUserRepository
    {
        int Add(User user);
        User Get(string username);
        bool Remove(string username);
        bool ChangePassword(string username, string passwordHash);
    }
}
