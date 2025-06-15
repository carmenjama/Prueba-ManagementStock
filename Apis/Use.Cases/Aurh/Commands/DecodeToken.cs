using ManagementProducts.Use.Cases.Aurh.Interfaces;
using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.Use.Cases.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ManagementProducts.SharedDto.Dto;
using System.Security.Claims;

namespace ManagementProducts.Use.Cases.Aurh.Commands
{
    public class DecodeToken : IDecodeToken
    {
        private JwtTokenContext _jwtTokenContext;
        public IDecodeToken WithContext(JwtTokenContext jwtTokenContext)
        {
            _jwtTokenContext = jwtTokenContext;
            return this;
        }

        public UseCaseResult<TokenDto, Failure> Execute(string token)
        {
            TokenDto data = new TokenDto();
            try
            {
                var usuario = new JwtSecurityTokenHandler().ValidateToken(token, Credentials(), out SecurityToken securityToken);
                if (usuario?.Identities?.FirstOrDefault()?.Claims is not null)
                {
                    foreach (var item in usuario?.Identities?.FirstOrDefault()?.Claims)
                    {
                        switch (item.Type)
                        {
                            case ClaimTypes.Name:
                                data.User = item.Value;
                                break;
                        }
                    }
                }
                if (string.IsNullOrEmpty(data.User))
                    return new UseCaseError { Reason = "Unauthorized", Message = "Token inválido" };
                return data;
            }
            catch (SecurityTokenException)
            {
                return new UseCaseError { Reason = "Unauthorized", Message = "Credenciales incorrectas" };
            }
        }

        private TokenValidationParameters Credentials()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_jwtTokenContext.Key));

            return new TokenValidationParameters()
            {
                ValidAudience = _jwtTokenContext.AudienceToken,
                ValidIssuer = _jwtTokenContext.IsUserToken,
                ValidateLifetime = _jwtTokenContext.Expire,
                IssuerSigningKey = securityKey
            };
        }
    }
}
