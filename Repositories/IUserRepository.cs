using CRUD.Models;
using System.Threading.Tasks;

namespace CRUD.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

        public Task UpdateUserName(User user);
        public Task UpdateUserPassword(User user);
        public void LoadPendencias(User user);
        public ResetPasswordToken GetLastToken(User user);
    }
}
