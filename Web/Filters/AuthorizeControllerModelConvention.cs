using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Web.Filters;

public class AuthorizeControllerModelConvention : IControllerModelConvention
{
    private readonly string _namespace;
    private readonly AuthorizationPolicy _policy;

    public AuthorizeControllerModelConvention(string @namespace, AuthorizationPolicy policy)
    {
        _namespace = @namespace;
        _policy = policy;
    }

    public void Apply(ControllerModel controller)
    {
        // Only apply to controllers in the specified namespace
        if (controller.ControllerType.Namespace != null && 
            controller.ControllerType.Namespace.StartsWith(_namespace))
        {
            controller.Filters.Add(new AuthorizeFilter(_policy));
        }
    }
}
