using Microsoft.EntityFrameworkCore;
using Store.DataAcess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Store.DataAcess.Repositories.Base
{
    public class BaseEFRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _ctx;
        private readonly DbSet<TEntity> _dbSet;

        public BaseEFRepository(DbContext context)
        {
            _ctx = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Create(TEntity model)
        {
            _dbSet.Add(model);

            _ctx.SaveChanges();

            return model;
        }

        public void Delete(TEntity model)
        {
            _dbSet.Attach(model);

            _dbSet.Remove(model);

            _ctx.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public TEntity GetById(long id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Update(TEntity model)
        {
            _ctx.Entry(model).State = EntityState.Modified;
            _ctx.SaveChanges();

            return model;
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}
