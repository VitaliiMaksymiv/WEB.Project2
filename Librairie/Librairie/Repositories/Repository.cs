namespace Librairie.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        public virtual EntityEntry<T> Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Delete(T entity)
        {
            if (_dbSet.Contains(entity))
            {
                _dbSet.Remove(entity);
            }
        }

        public virtual void Delete(params object[] keys)
        {
            T existing = _dbSet.Find(keys);
            if (existing != null)
            {
                _dbSet.Remove(existing);
            }
        }

        public virtual IQueryable<T> Get()
        {
            return _dbSet;
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public virtual T Get(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsEnumerable();
        }

        public virtual int Count()
        {
            return _dbSet.Count();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}