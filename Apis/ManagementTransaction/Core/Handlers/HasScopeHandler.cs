using Microsoft.AspNetCore.Authorization;

namespace ManagementTransaction.Api.Core.Handlers
{
    public class HasScopeHandler : AuthorizationHandler<ScopeRequirementHandler>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeRequirementHandler requirement)
        {
            //If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            //Split the scopes string into an array
            //var scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer).Value.Split(' ');
            var scopes = context.User.FindAll(c => c.Type == "scope" && c.Issuer == requirement.Issuer).Select(x => x.Value.Split(" ")[0]).ToList();

            //Succeed if the scope array contains the required scope
            if (scopes.Contains(requirement.Scope))
                context.Succeed(requirement);
            else
                throw new InvalidOperationException("The required scope is not present in the token.");

            return Task.CompletedTask;
        }
    }
}
