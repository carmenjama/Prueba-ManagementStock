using AutoMapper;
using ManagementProducts.Api.Controllers.Responses;
using ManagementProducts.SharedDto.Dto;

namespace ManagementProducts.Api.Core.Settings
{
    public class AutoMapperSettings : Profile
    {
        public AutoMapperSettings()
        {
            CreateMap<TypeTransactionResponsse, TypeTransactionDto>().ReverseMap();
        }
    }
}
