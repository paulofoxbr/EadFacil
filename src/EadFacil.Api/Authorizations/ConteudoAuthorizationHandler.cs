using Microsoft.AspNetCore.Authorization;

namespace EadFacil.Api.Authorizations;

public class ConteudoAuthorizationHandler : AuthorizationHandler<ConteudoAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ConteudoAuthorizationRequirement requirement)
    {
        var user = context.User;
        if (!user.Identity.IsAuthenticated)
        {
            context.Fail();
            return Task.CompletedTask;
        }
        if (user.IsInRole("Admin"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        context.Fail();
        return Task.CompletedTask;
    }
}