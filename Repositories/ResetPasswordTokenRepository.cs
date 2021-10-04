using CRUD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Repositories
{
    public class ResetPasswordTokenRepository : GenericRepository<ResetPasswordToken>, IResetPasswordTokenRepository
    {
        public ResetPasswordTokenRepository(ApiContext context) : base(context) { }

        public async Task<ResetPasswordToken> GetTokenByTokenString(string token)
        {
            return await context.ResetPasswordToken.Where(t => t.Token == token).FirstOrDefaultAsync();
        }
    }
}
