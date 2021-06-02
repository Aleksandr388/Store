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
        private readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseEFRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task CreateAsync(TEntity model)
        {
            await _dbSet.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity model)
        {
            _dbSet.Attach(model);
            _dbSet.Remove(model);

            await _context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {

            var result = await _dbSet.ToListAsync();

            return result;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task UpdateAsync(TEntity model)
        {
            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async  Task SaveChagesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
