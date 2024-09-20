using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyProj.WebApi.Data;
using MyProj.WebApi.Interfaces;

namespace MyProj.WebApi.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbContextTransaction _transaction;
        private AppDbContext _appDb;
        public IProductRepository Product { get; set; }
        
        public UnitOfWork(AppDbContext appDb)
        {
            _appDb = appDb;
           // Product = new ProductRepository(appDb);

        }

        public void  BeginTrasaction()
        {
            _transaction =  _appDb.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            await _appDb.SaveChangesAsync();
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            _appDb.Dispose();
        }

        public async Task RollBackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }

        public Task<int> SaveChangesAsync()
        => _appDb.SaveChangesAsync();
    }
}
