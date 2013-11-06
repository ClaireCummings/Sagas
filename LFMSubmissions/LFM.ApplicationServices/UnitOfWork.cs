using System;
using System.Transactions;

namespace LFM.ApplicationServices
{
    /// <summary>
    /// Handle unit of work 
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private readonly TransactionScope _transaction;

        public UnitOfWork()
        {
            _transaction = new TransactionScope();
        }

        public void Complete()
        {
            _transaction.Complete();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}