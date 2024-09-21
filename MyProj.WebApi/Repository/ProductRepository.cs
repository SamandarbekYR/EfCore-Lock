using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyProj.WebApi.Data;
using MyProj.WebApi.DTOs;
using MyProj.WebApi.Entities.Products;
using MyProj.WebApi.Interfaces;

namespace MyProj.WebApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Product> _dbSet;
        private readonly IMapper _mapper;
        //private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private static readonly object _lockObject = new object();
        public ProductRepository(AppDbContext context, IMapper mapper )
        {
            _context = context;
            _dbSet = context.Set<Product>();
            _mapper = mapper;
        }

        public async Task<bool> AddProductAsync(AddProductDto product)
        {
            // await _semaphore.WaitAsync();
            Monitor.Enter(_lockObject);
            try
            {
                var existingProduct = await _dbSet.AsNoTracking()
                                                  .FirstOrDefaultAsync(p => p.Name == product.Name);
                if (existingProduct != null)
                {
                    return false;
                }

                var mapProduct = _mapper.Map<Product>(product);
                await _dbSet.AddAsync(mapProduct);
                await _context.SaveChangesAsync();

                await Task.Delay(100);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Monitor.Exit(_lockObject);
                //_semaphore.Release();
            }
        }
    }
}
