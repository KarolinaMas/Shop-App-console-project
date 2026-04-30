using Shop.Data;
using Shop.Entities;

namespace Shop.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopDbContext dbContext;

        public UserRepository(ShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(User user)
        {
            var entityEntry = dbContext.Users.Add(user);

            dbContext.SaveChanges();

            return entityEntry.Entity.Id;
        }

        public User Get(string username)
        {
            return dbContext.Users.SingleOrDefault(u => u.Username == username); // SingleOrDefault mes klaida jei tu paciu username bus keli
        }

        public bool Remove(string username)
        {
            var user = dbContext.Users.SingleOrDefault(u => u.Username == username);

            if (user == null)
                return false;

            dbContext.Users.Remove(user);

            dbContext.SaveChanges();

            return true;
        }

        public bool ChangePassword(string username, string passwordHash)
        {
            var user = dbContext.Users.SingleOrDefault(u => u.Username == username);

            if (user == null)
            {
                return false;
            }

            user.PasswordHash = passwordHash;

            dbContext.SaveChanges();

            return true;
        }
    }
}
