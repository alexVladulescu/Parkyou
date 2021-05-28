using System.Linq;
using Parkyou.Data;
using Parkyou.Interfaces;
using Parkyou.Models;

namespace Parkyou.Repositories
{
    public partial class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public User GetByEmail(string email)
        {
            return _context.AppUsers.FirstOrDefault(u => u.Email == email.Trim());
        }

        public User GetByUsername(string username)
        {
            return _context.AppUsers.FirstOrDefault(u => u.UserName == username.Trim());
        }
    }
}