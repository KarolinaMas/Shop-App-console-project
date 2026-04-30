using Shop.Entities;

namespace Shop.Services
{
    public interface IUserService
    {
        int Register(string username, string password, string repeatPassword);
        User Login(string username, string password);
        bool Remove(string username);
        bool ChangePassword(
            string username,
            string oldPassword,
            string newPassword,
            string repeatPassword
        );
    }
}
