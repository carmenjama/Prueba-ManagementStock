using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.Use.Cases.Shared;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Use.Cases.Aurh.Interfaces
{
    [ScopedService]
    public interface IGenerateToken
    {
        IGenerateToken WithContext(JwtTokenContext jwtTokenContext);
        UseCaseResult<string, Failure> Execute(string user, string pass, string appName);
    }
}
