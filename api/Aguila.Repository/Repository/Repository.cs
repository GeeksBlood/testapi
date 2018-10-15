using Aguila.DAL;
using Aguila.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Repository.Repository
{
    /// <summary>
    /// This generic class implement IRepository interface for CURD operation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private AguilaContext entities = null;
        public Repository(AguilaContext _entities)
        {
            entities = _entities;
        }
        public async Task<IEnumerable<T>> Get()
        {
            IQueryable<T> query = entities.Set<T>();
            return await query.ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            return await entities.Set<T>().FindAsync(id);
        }

        public bool Insert(T data)
        {
            entities.Set<T>().Add(data);
            return true;
        }

        public bool Update(int id, T data)
        {
            entities.Entry(data).State = EntityState.Modified;
            return true;
        }

        public bool Delete(int id)
        {
            var result = entities.Set<T>().Find(id);
            if (result != null)
            {
                entities.Set<T>().Remove(result);
                return true;
            }
            return false;
        }
        public async Task<bool> FindByID(int id)
        {
            var result = await entities.Set<T>().FirstOrDefaultAsync();
            if (result != null)
            {
                return true;
            }
            return false;
        }
        
    }
}
