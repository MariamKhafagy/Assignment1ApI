using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer
{
    public interface IProductService
    {
        //Get All Products 
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        //Get All Producr By Id 
        Task<ProductDto> GetProductByIdAsync(int id);

        //Get All Types 
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        // Get All Brands 
        Task<IEnumerable<BrandDto>>GetAllBrandsAsync();
    }
}
