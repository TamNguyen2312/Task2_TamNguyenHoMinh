using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepoBase<T> GetRepo<T>() where T : class;
        void SaveChanges();
        void BeginTransactionAsync();
        void CommitTransactionAsync();
        void RollBackAsync();
    }
}
