using Microsoft.AspNetCore.Authorization;

namespace EadFacil.Api.Authorizations;

public class AlunoAuthorizationHandler : AuthorizationHandler<AlunoAuthorizationRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AlunoAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AlunoAuthorizationRequirement requirement)
    {
        var user = context.User;
        if (user==null || !user.Identity.IsAuthenticated)
        {
            context.Fail();
            return Task.CompletedTask;
        }
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}