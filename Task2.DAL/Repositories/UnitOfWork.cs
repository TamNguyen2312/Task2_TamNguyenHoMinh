using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PubsContext _masterContext;
        private IDbContextTransaction _transaction;
        private readonly Dictionary<Type, object> _repositories;
        public UnitOfWork(PubsContext masterContext)
        {
            _masterContext = masterContext;
            _repositories = new Dictionary<Type, object>();
        }

        public void BeginTransactionAsync()
        {
            _transaction = _masterContext.Database.BeginTransaction();
        }

        public void CommitTransactionAsync()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null!;
            }
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _masterContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public IRepoBase<T> GetRepo<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return _repositories[typeof(T)] as IRepoBase<T>;
            }

            var repo = new RepoBase<T>(_masterContext);
            _repositories.Add(typeof(T), repo);
            return repo;
        }

        public void RollBackAsync()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public void SaveChanges()
        {
            _masterContext.SaveChanges();
        }
    }

    
}

