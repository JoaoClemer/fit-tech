using FitTech.Domain.Repositories;
using FitTech.Infrastructure.Context;

namespace FitTech.Infrastructure.RepositoryAccess
{
    internal class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly FitTechContext _context;
        private bool _disposed;

        public UnitOfWork(FitTechContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if(!_disposed && disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
        
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

       
    }
}
