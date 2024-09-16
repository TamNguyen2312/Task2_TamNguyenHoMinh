using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task2.DAL.Repositories
{
    public class RepoBase<T> : IRepoBase<T> where T : class
    {

        private readonly PubsContext _context;
        protected readonly DbSet<T> _dbSet;
        public RepoBase(PubsContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            if (_context.Entry<T>(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        private IQueryable<T> Get(Expression<Func<T, bool>> predicate = null,
                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                  bool tracked = true,
                                  params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            includeProperties = includeProperties.Distinct().ToArray();
            Expression<Func<T, object>>[] array = includeProperties;
            if (includeProperties?.Any() ?? false)
            {
                foreach (var navigationProperty in array)
                {
                    query = query.Include(navigationProperty);
                }
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }


            return query;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool tracked = true, params Expression<Func<T, object>>[] includeProperties)
        {
            return Get(predicate, orderBy, tracked, includeProperties).ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool tracked = true, params Expression<Func<T, object>>[] includeProperties)
        {
            return Get(predicate, orderBy, tracked, includeProperties).FirstOrDefault();
        }

        public void Update(T entity)
        {
            if (_context.Entry<T>(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Update(entity);
        }
    }
}
