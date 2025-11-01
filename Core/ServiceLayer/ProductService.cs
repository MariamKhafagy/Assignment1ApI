using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstractionLayer;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ProductService (IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetALLASync();
            var brandsDtos = _mapper.Map<IEnumerable<BrandDto>>(brands);
           return brandsDtos;
           
        }

        public  async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetALLASync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public  async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
           var types= await _unitOfWork.GetRepository<ProductType, int>().GetALLASync();
            return _mapper.Map<IEnumerable<TypeDto>>(types);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetBYIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }
    }
}
