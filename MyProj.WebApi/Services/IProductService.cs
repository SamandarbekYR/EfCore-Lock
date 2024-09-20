using MyProj.WebApi.DTOs;
using MyProj.WebApi.Entities.Products;

namespace MyProj.WebApi.Services
{
    public interface IProductService
    {
        Task<bool> AddProduct(Product dto);
    }
}
