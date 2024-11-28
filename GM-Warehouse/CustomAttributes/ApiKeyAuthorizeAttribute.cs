using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GMWarehouse.CustomAttributes;

public class ApiKeyAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private const string ApiKeyHeaderName = "X-API-KEY";
    private const string AllowedApiKey = "TestKey";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!AllowedApiKey.Equals(extractedApiKey))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}