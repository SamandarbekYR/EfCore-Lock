
using Microsoft.EntityFrameworkCore;
using MyProj.WebApi.DTOs;
using MyProj.WebApi.Entities.Products;
using MyProj.WebApi.Interfaces;

namespace MyProj.WebApi.Services
{
    public class ProductServcie : IProductService
    {
        private IUnitOfWork _unitOfWork;

        public ProductServcie(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddProduct(Product dto)
        {
            //try
            //{
            //    _unitOfWork.BeginTrasaction();

            //    var result = _unitOfWork.Product.GetByName(dto.Name);

            //    if(result.Result)
            //    {
            //        await _unitOfWork.Product.AddProduct(dto);
            //        await _unitOfWork.SaveChangesAsync();
                    
            //    }
            //    await _unitOfWork.CommitAsync();
               
            //    return result.Result;
            //}
            //catch (Exception ex)
            //{

            //    await _unitOfWork.RollBackAsync();
            //    return false;
            //}
            return true;
        }
    }
}
