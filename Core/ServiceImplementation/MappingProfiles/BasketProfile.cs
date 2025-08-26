using AutoMapper;
using Domain_Layer.Models.BasketModule;
using Shared.DataTransferObjects.BasketModuleDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Implementation.MappingProfiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket,BasketDTo>().ReverseMap();
            CreateMap<BasketItem,BasketItemDTo>().ReverseMap();
        }
    }
}
