
using Application.Entities;
using Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;

namespace Web.Filters;

public class IsAdminHandler : AuthorizationHandler<IsAdminRequirement> {

    private readonly IUserService _userService;

    public IsAdminHandler(IUserService service) {
        _userService = service;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement) {
        // _userService.GetUserByEmail();
        
        if(context.User.Identity.Name.Equals("admin"))
            context.Succeed(requirement);
        else
            context.Fail();
    }
}