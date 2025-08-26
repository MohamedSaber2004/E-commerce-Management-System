using AutoMapper;
using AutoMapper.Execution;
using Domain_Layer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjecst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Implementation.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            else
            {
                var Url = $"{configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
                return Url;
            }
        }
    }
}
