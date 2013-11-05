using System;

namespace LFM.ApplicationServices
{
    /// <summary>
    /// Handle unit of work 
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        public void Complete()
        {
            // Surround with transactional unit of work
        }

        public void Dispose()
        {
            
        }
    }
}