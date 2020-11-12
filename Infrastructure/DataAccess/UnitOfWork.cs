using Application.Services;
using System;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly InternationalBankContext _dbContext;
        private bool _disposed;

        public UnitOfWork(InternationalBankContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveAsync()
        {
            int rows = await _dbContext.SaveChangesAsync();
            return rows;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _dbContext.Dispose();

            _disposed = true;
        }
    }
}
