using CRUD.Models;
using System.Threading.Tasks;

namespace CRUD.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApiContext context) : base(context) {}

        public async Task UpdateUserName(User user)
        {
            context.Entry(user).Property(us => us.Nome).IsModified = true;
            await context.SaveChangesAsync();
        }
        public async Task UpdateUserPassword(User user)
        {
            context.Entry(user).Property(us => us.Senha).IsModified = true;
            await context.SaveChangesAsync();
        }
    }
}
