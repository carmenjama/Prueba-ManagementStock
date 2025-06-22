using AutoMapper;
using ManagementProducts.SharedDto.Dto;
using ManagementTransaction.Api.Controllers.Payload;
using ManagementTransaction.Api.Controllers.Responses;

namespace ManagementTransaction.Api.Core.Settings
{
    public class AutoMapperSettings : Profile
    {
        public AutoMapperSettings()
        {
            CreateMap<TypeTransactionResponsse, TypeTransactionDto>().ReverseMap();
            CreateMap<TransactionResponsse, TransactionDto>().ReverseMap();
            CreateMap<TransactionPayload, TransactionDto>().ReverseMap();
        }
    }
}
