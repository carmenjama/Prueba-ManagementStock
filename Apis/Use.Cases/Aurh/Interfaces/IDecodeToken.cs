using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Shared;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Use.Cases.Aurh.Interfaces
{
    [ScopedService]
    public interface IDecodeToken
    {
        IDecodeToken WithContext(JwtTokenContext jwtTokenContext);
        UseCaseResult<TokenDto, Failure> Execute(string token);
    }
}
