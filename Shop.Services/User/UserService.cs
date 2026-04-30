using Microsoft.AspNetCore.Identity;
using Shop.Entities;
using Shop.Repositories;

namespace Shop.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly PasswordHasher<User> passwordHasher;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.passwordHasher = new PasswordHasher<User>();
        }

        public int Register(string username, string password, string repeatPassword)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required");

            if (password != repeatPassword)
                throw new ArgumentException("Passwords do not match");

            var existingUser = userRepository.Get(username);

            if (existingUser != null)
                throw new ArgumentException("User with this username already exists");

            var newUser = new User { Username = username };

            newUser.PasswordHash = passwordHasher.HashPassword(newUser, password);

            var id = userRepository.Add(newUser);

            return id;
        }

        public User Login(string username, string password)
        {
            var user = userRepository.Get(username);

            if (user == null)
                throw new Exception("Invalid username or password"); // paieskot tinkamesnio exeption tipo, jei yra

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result != PasswordVerificationResult.Success)
                throw new Exception("Invalid username or password");

            return user;
        }

        public bool Remove(string username)
        {
            return userRepository.Remove(username);
        }

        public bool ChangePassword(
            string username,
            string oldPassword,
            string newPassword,
            string repeatPassword
        )
        {
            if (newPassword != repeatPassword)
                throw new ArgumentException("Passwords do not match");

            var user = userRepository.Get(username);

            if (user == null)
                throw new Exception("User not found");

            var verify = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword);

            if (verify != PasswordVerificationResult.Success)
                throw new Exception("Old password is incorrect");

            var passwordHash = passwordHasher.HashPassword(user, newPassword);

            return userRepository.ChangePassword(username, passwordHash);
        }
    }
}
