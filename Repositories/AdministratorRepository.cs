using System.Linq;
using Parkyou.Data;
using Parkyou.Interfaces;
using Parkyou.Models;

namespace Parkyou.Repositories
{
    public partial class AdministratorRepository : GenericRepository<Administrator>, IAdministratorRepository
    {
        public AdministratorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Administrator GetByEmail(string email)
        {
            return _context.Administrators.FirstOrDefault(p => p.Email == email.Trim().ToLower());
        }

        public Administrator GetByUsername(string username)
        {
            return _context.Administrators.FirstOrDefault(p => p.UserName == username.Trim());
        }
    }
}