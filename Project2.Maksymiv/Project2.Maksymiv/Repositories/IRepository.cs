namespace Librairie.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> Get();

        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        T Get(params object[] keyValues);

        IEnumerable<T> GetAll();

        EntityEntry<T> Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(params object[] keys);

        void Update(T entity);

        int Count();
    }
}