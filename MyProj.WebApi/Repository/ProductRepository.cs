using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyProj.WebApi.Data;
using MyProj.WebApi.DTOs;
using MyProj.WebApi.Entities.Products;
using MyProj.WebApi.Interfaces;

namespace MyProj.WebApi.Repository
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Product> _dbSet;
        private readonly IMapper _mapper;
        private IDbContextTransaction _dbTransaction;
        public ProductRepository(AppDbContext context, IMapper mapper )
        {
            _context = context;
            _dbSet = context.Set<Product>();
            _mapper = mapper;
        }

        public async Task<bool> AddProductAsync(AddProductDto product)
        {
            _dbTransaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead);
            await _context.Database.ExecuteSqlRawAsync($"LOCK TABLE \"Products\" IN EXCLUSIVE MODE;");

            var existingProduct = await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name == product.Name);

            if (existingProduct != null)
            {
                await _dbTransaction.RollbackAsync();
                return false;
            }

            var mapProduct = _mapper.Map<Product>(product);
            await _dbSet.AddAsync(mapProduct);
            await _context.SaveChangesAsync();
            await _dbTransaction.CommitAsync();
            
            return true;
        }

        public void Dispose()
        {
            if (_dbTransaction != null)
            {
                _dbTransaction.Dispose();
            }
        }
    }
}
