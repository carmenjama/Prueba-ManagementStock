using AutoMapper;
using ManagementProducts.SharedDto.Dto;
using ManagementTrans.Api.Controllers.Payload;
using ManagementTrans.Api.Controllers.Responses;

namespace ManagementTrans.Api.Core.Settings
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
