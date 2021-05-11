using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public Task CreateAsync(TEntity model);
        public Task UpdateAsync(TEntity model);
        public Task DeleteAsync(TEntity model);
        public Task<TEntity> GetByIdAsync(long id);
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        public Task SaveChagesAsync();
    }
}
