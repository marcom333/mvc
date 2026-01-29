using Microsoft.AspNetCore.Authorization;

namespace Web.Filters;

public class IsAdminRequirement : IAuthorizationRequirement
{
    public const string PolicyName = "IsAdmin";
}