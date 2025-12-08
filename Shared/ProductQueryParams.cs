using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
          
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public  ProductSoritngOptions SoritngOption { get; set; }
    
        public string? SearchValue { get; set; }

        #region Pagination
        private const int DefultPageSize = 5;
        private const int MaxPageSize = 10;


        public int PageIndex { get; set; } = 1;
        private int pageSize = DefultPageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? DefultPageSize : value ; }
        }

        #endregion




    }
}
