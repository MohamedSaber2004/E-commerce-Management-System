using AutoMapper;
using Domain_Layer.Models.ProductModule;
using Shared.DataTransferObjecst;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Implementation.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(destination => destination.ProductBrand, Options => Options.MapFrom(source => source.ProductBrand.Name))
                .ForMember(destination => destination.ProductType, Options => Options.MapFrom(source => source.ProductType.Name))
                .ForMember(destination => destination.PictureUrl, Options => Options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDTO>();

            CreateMap<ProductType,TypeDTO>();
        }
    }
}
