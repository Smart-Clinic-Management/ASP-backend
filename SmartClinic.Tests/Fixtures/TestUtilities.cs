using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;
using System.Collections.Generic;
using System.Security.Claims;

namespace SmartClinic.Tests.Fixtures;

public static class TestUtilities
{
    // Helper method to create a controller context with optional user claims
    public static ControllerContext CreateControllerContext(ClaimsPrincipal? user = null)
    {
        var httpContext = new DefaultHttpContext
        {
            User = user ?? new ClaimsPrincipal(new ClaimsIdentity())
        };

        return new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    // Helper method to create a ClaimsPrincipal for authentication tests
    public static ClaimsPrincipal CreateClaimsPrincipal(string userId = "test-user", string role = "User")
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Role, role)
        };

        return new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthType"));
    }

    // Helper method to create a service provider with mocked dependencies
    public static IServiceProvider CreateServiceProvider(Mock<IUnitOfWork> mockUnitOfWork)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IUnitOfWork>(mockUnitOfWork.Object);

        // Add other mocked services as needed

        return services.BuildServiceProvider();
    }
}