using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;

namespace checkboxtest.DAL
{
    interface IRepository<TEntity> : IDisposable
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
                orderBy = null,
            string includeProperties = "");
        IQueryable<TEntity> GetAll();
        TEntity GetById(object id);
        void Delete(TEntity entity);
        void Delete(object id);
        void Add(TEntity entity);

    }
}
