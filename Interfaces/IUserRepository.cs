using Parkyou.Models;

namespace Parkyou.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetByEmail(string email);
        public User GetByUsername(string username);
    }
}