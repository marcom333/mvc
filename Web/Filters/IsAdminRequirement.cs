using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Web.Filters;

public class IsAdminRequirement : IAuthorizationRequirement
{
    public const string PolicyName = "IsAdmin";
}