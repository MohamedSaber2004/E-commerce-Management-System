using AutoMapper;
using Domain_Layer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects.AuthenticationModuleDTos;
using Shared.DataTransferObjects.OrderModuleDTos;

namespace Service_Implementation.MappingProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTo, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDTo>()
                .ForMember(destination => destination.DeliveryMethod, Options => Options.MapFrom(source => source.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDTo>()
                .ForMember(destination => destination.ProductName,
                    Options => Options.MapFrom(source => source.Product.ProductName))
                .ForMember(destination => destination.PictureUrl,
                    Options => Options.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDTo>()
                .ForMember(destination => destination.Cost, Options => Options.MapFrom(source => source.Price));
        }
    }
}
