using AutoMapper;
using Domain_Layer.Exceptions;
using Domain_Layer.Models.ProductModule;
using Domain_Layer.Repository_Interfaces;
using Service_Abstraction;
using Service_Implementation.Specifications;
using Shared;
using Shared.DataTransferObjecst;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Implementation
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDTO>> GetAllBrandAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();

            var Brands = await Repo.GetAllAsync();

            var BrandDto = _mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDTO>>(Brands);
            return BrandDto;
        }

        public async Task<PaginatedResult<ProductDTO>> GetAllProductAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var specifications = new ProductWithBrandAndTypeSpecifications(queryParams);
            var Products = await Repo.GetAllAsync(specifications);
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Products);
            var ProductCount = Data.Count();
            var CountSpecification = new ProductCountSpecification(queryParams);
            var TotalCount = await Repo.CountAsync(CountSpecification);
            return new PaginatedResult<ProductDTO>(queryParams.PageNumber, ProductCount, TotalCount, Data);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            var TypesDto = _mapper.Map<IEnumerable<ProductType>,IEnumerable<TypeDTO>>(Types);
            return TypesDto;
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
            if(Product is null)
            {
                throw new ProductNotFoundException(id);
            }
            return _mapper.Map<Product,ProductDTO>(Product);
        }
    }
}
