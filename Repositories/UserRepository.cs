using CRUD.Models;

namespace CRUD.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApiContext context) : base(context) {}
    }
}
