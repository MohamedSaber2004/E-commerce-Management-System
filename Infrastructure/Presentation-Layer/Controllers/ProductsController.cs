using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Attributes;
using Service_Abstraction;
using Shared;
using Shared.DataTransferObjecst;
using Shared.DataTransferObjects;

namespace Presentation_Layer.Controllers
{
    public class ProductsController(IServiceManager _serviceManager) : ApiController
    {
        // Get All Products
        [HttpGet]
        [Cache]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var products = await _serviceManager.ProductService.GetAllProductAsync(queryParams);
            return Ok(products);
        }

        // Get All Product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }


        // Get All Types
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }


        // Get All Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandAsync();
            return Ok(brands);
        }
    }
}
