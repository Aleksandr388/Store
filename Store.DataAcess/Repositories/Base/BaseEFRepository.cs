using Microsoft.EntityFrameworkCore;
using Store.DataAcess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.Base
{
    public class BaseEFRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _ctx;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseEFRepository(DbContext context)
        {
            _ctx = context;
            _dbSet = _ctx.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity model)
        {
            await _dbSet.AddAsync(model);

            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity model)
        {
            _dbSet.Remove(model);

            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(TEntity model)
        {
            _ctx.Entry(model).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();

        }

        public async  Task SaveChagesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
