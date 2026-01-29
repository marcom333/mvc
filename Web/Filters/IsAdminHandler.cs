using Microsoft.AspNetCore.Authorization;

namespace Web.Filters;

public class IsAdminHandler : AuthorizationHandler<IsAdminRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
    {
        context.Succeed(requirement); //Aquí ponerle un IF de que si el usuario es admin 
    }
}