using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NC_Record_Shop_API_Mini_Project.Filters;
namespace NC_Record_Shop_API_Mini_Project.Tests;

public class ApiKeyAttributeTests
{
    private AuthorizationFilterContext CreateContext(string? configuredKey, string? providedKey)
    {
        var configValues = new Dictionary<string, string?>();
        if (configuredKey != null) configValues["ApiKey"] = configuredKey;
        IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(configValues).Build();

        var services = new ServiceCollection();
        services.AddSingleton(config);

        var httpContext = new DefaultHttpContext { RequestServices = services.BuildServiceProvider() };
        if (providedKey != null) httpContext.Request.Headers[ApiKeyAttribute.HeaderName] = providedKey;

        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        return new AuthorizationFilterContext(actionContext, new List<IFilterMetadata>());
    }

    [Fact]
    public void ValidKey_ShouldAllowRequest()
    {
        var context = CreateContext("secret", "secret");

        new ApiKeyAttribute().OnAuthorization(context);

        Assert.Null(context.Result);
    }

    [Fact]
    public void WrongKey_ShouldReturnUnauthorized()
    {
        var context = CreateContext("secret", "wrong");

        new ApiKeyAttribute().OnAuthorization(context);

        Assert.IsType<UnauthorizedResult>(context.Result);
    }

    [Fact]
    public void MissingKey_ShouldReturnUnauthorized()
    {
        var context = CreateContext("secret", null);

        new ApiKeyAttribute().OnAuthorization(context);

        Assert.IsType<UnauthorizedResult>(context.Result);
    }
}
