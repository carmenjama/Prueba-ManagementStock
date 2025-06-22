using Microsoft.AspNetCore.Authorization;

namespace ManagementTrans.Api.Core.Handlers
{
    ///<summary>
    /// This authorisation handler will bypass all requirements
    /// </summary>
    public class HasAnonymousScopeHanddler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (IAuthorizationRequirement requirement in context.PendingRequirements.ToList())
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
