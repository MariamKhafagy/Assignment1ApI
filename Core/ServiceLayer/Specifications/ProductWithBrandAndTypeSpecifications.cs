using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product ,int>
    {

        //Products And Brands with Types and Brands
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) 
            :base(p=>(!queryParams.BrandId.HasValue || p.BrandId== queryParams.BrandId)
                    && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
                    &&(string.IsNullOrWhiteSpace(queryParams.SearchValue)||p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {//whre (p=>p.BrandId==brandId && TypeId==typeId && p.Name.Contains("chicken))
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch (queryParams.SoritngOption)
            {
                case ProductSoritngOptions.NameAsc:
                    AddOrderby(p => p.Name);
                    break;

                case ProductSoritngOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                case ProductSoritngOptions.PriceAsc:
                    AddOrderby(p => p.Price);
                    break;
                case ProductSoritngOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }

        public ProductWithBrandAndTypeSpecifications(int id ) : base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

    }
}
