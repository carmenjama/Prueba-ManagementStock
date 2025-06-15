using ManagementProducts.Use.Cases.Aurh.Interfaces;
using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.Use.Cases.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace ManagementProducts.Use.Cases.Aurh.Commands
{
    public class GenerateToken : IGenerateToken
    {
        private JwtTokenContext _jwtTokenContext;
        public IGenerateToken WithContext(JwtTokenContext jwtTokenContext)
        {
            _jwtTokenContext = jwtTokenContext;
            return this;
        }

        public UseCaseResult<string, Failure> Execute(string user, string pass, string appName)
        {
            if (user != _jwtTokenContext.UserName || pass != _jwtTokenContext.Pass)
                return new UseCaseError { Reason = "Unauthorized", Message = "Credenciales incorrectas" };

            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_jwtTokenContext.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                   new Claim(ClaimTypes.Name, user),
                   new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                   new Claim(ClaimTypes.GroupSid, appName),
               });

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
              audience: _jwtTokenContext.AudienceToken,
              issuer: _jwtTokenContext.IsUserToken,
              subject: claimsIdentity,
              notBefore: DateTime.Now,
              expires: DateTime.Now.AddMinutes(_jwtTokenContext.ExpireMinutes),
              signingCredentials: signingCredentials
              );

            return tokenHandler.WriteToken(jwtSecurityToken);
        }
    }
}
