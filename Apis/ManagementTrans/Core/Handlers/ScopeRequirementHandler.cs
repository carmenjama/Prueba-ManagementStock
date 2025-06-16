using Microsoft.AspNetCore.Authorization;

namespace ManagementProducts.Api.Core.Handlers
{
    public class ScopeRequirementHandler : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Scope { get; }

        public ScopeRequirementHandler(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer.EndsWith("/")
              ? issuer.Remove(issuer.Length - 1)
              : issuer
              ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}
