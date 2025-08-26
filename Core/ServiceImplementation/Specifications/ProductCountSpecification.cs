using Domain_Layer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Implementation.Specifications
{
    class ProductCountSpecification:BaseSpecifications<Product,int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams)
            : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
                  && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
                  && (string.IsNullOrWhiteSpace(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {
            
        }
    }
}
