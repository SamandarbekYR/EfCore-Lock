using MyProj.WebApi.DTOs;
using MyProj.WebApi.Entities.Products;

namespace MyProj.WebApi.Interfaces;

public interface IProductRepository
{
    Task<bool> AddProductAsync(AddProductDto product);
}
