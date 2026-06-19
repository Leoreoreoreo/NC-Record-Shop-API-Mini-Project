using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NC_Record_Shop_API_Mini_Project.Filters
{
    // Requires a matching X-Api-Key header (compared against the "ApiKey" config value)
    // for the action it is applied to. Used to protect the write endpoints.
    public class ApiKeyAttribute : Attribute, IAuthorizationFilter
    {
        public const string HeaderName = "X-Api-Key";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var configuredKey = context.HttpContext.RequestServices
                .GetService<IConfiguration>()?["ApiKey"];

            context.HttpContext.Request.Headers.TryGetValue(HeaderName, out var providedKey);

            if (string.IsNullOrEmpty(configuredKey) || providedKey.ToString() != configuredKey)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
