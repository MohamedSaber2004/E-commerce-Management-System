using Shared;
using Shared.DataTransferObjecst;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Abstraction
{
    public interface IProductService
    {
        // Get All Products
        Task<PaginatedResult<ProductDTO>> GetAllProductAsync(ProductQueryParams queryParams);

        // Get Product By Id
        Task<ProductDTO> GetProductByIdAsync(int id);

        // Get All Types
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();

        // Get All Brands
        Task<IEnumerable<BrandDTO>> GetAllBrandAsync();
    }
}
