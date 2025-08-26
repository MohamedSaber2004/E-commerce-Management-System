using AutoMapper;
using Domain_Layer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects.OrderModuleDTos;

namespace Service_Implementation.MappingProfiles
{
    class OrderItemPictureUrlResolver(IConfiguration _configuration): IValueResolver<OrderItem, OrderItemDTo, string>
    {
        public string Resolve(OrderItem source, OrderItemDTo destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.Product.PictureUrl)) 
                return string.Empty;
            else
            {
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
                return Url;
            }
        }
    }
}
