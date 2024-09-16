using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task2.DAL.Repositories
{
    public interface IRepoBase<T> where T : class
    {
        public T Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null,
                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                bool tracked = true,
                                                params Expression<Func<T, object>>[] includeProperties);
        public T GetSingle(Expression<Func<T, bool>> predicate = null,
                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                bool tracked = true,
                                                params Expression<Func<T, object>>[] includeProperties);
    }
}
