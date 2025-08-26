using Domain_Layer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Implementation.Specifications
{
    class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int> 
    {
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) 
          : base(p =>(!queryParams.BrandId.HasValue || p.BrandId==queryParams.BrandId) 
                  && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
                  && (string.IsNullOrWhiteSpace(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch(queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    break;

            }

            ApplyPagination(queryParams.PageSize, queryParams.PageNumber);
        }

        public ProductWithBrandAndTypeSpecifications(int id):base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

    }
}
