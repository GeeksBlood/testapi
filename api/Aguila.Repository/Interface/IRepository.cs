using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Repository.Interface
{
    /// <summary>
    /// This is generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(int id);
        bool Insert(T data);
        bool Update(int id, T data);
        bool Delete(int id);
        Task<bool> FindByID(int id);
    }
}
