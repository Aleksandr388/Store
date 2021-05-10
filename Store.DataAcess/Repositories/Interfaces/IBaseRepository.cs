using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Store.DataAcess.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity model);
        TEntity Update(TEntity model);
        void Delete(TEntity model);
        TEntity GetById(long id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
    }
}
