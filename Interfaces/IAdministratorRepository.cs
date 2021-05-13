using Parkyou.Models;

namespace Parkyou.Interfaces
{
    public interface IAdministratorRepository : IGenericRepository<Administrator>
    {
        public Administrator GetByEmail(string email);
        public Administrator GetByUsername(string username);
    }
}