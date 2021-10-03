using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int Id);
        Task Insert(T obj);
        Task Update(T obj);
        Task Delete(int Id);
    }
}
