
using AutoMapper;
using Domain_Layer.Models.IdeneityModule;
using Shared.DataTransferObjects.AuthenticationModuleDTos;

namespace Service_Implementation.MappingProfiles
{
    public class IdentityProfile:Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address, AddressDTo>().ReverseMap();
        }
    }
}
