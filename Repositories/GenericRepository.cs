using CRUD.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApiContext context = null;
        public GenericRepository(ApiContext context)
        {
            this.context = context;
        }

        public async Task Delete(int Id)
        {
            var entity = await GetById(Id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int Id)
        {
            return await context.Set<T>().FindAsync();
        }

        public async Task Insert(T obj)
        {
            await context.Set<T>().AddAsync(obj);
            await context.SaveChangesAsync();
        }

        public async Task Update(T obj)
        {
            context.Set<T>().Update(obj);
            await context.SaveChangesAsync();
        }
    }
}
