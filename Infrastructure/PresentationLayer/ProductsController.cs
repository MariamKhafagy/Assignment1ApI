using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        //Get All Products 
        [HttpGet] //Get :: BaseUrl/api/Products
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var products =await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }


        //Get Products By Id 
        [HttpGet("{id}")] //Get :: BaseUrl/api/Products/4
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        { 
          var product   =  await  _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

        //Get All Brands 
        [HttpGet("brands")] //Get :: BaseUrl/api/Products/brands
        public async Task<ActionResult<BrandDto>> GetAllBrands()
        { 
          var brands= await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }

        //Get All Types 
        [HttpGet("types")] //Get :: BaseUrl/api/Products/types
        public async Task<ActionResult<TypeDto>> GetAllTypes()
        {
          var types= await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }


    }
}
