using Microsoft.AspNetCore.Identity;
using SmartClinic.Domain.Entities;
using SmartClinic.Infrastructure.Data;

namespace SmartClinic.API;
public static class ModuleAPIDependencies
{
    public static IServiceCollection AddAPIDependencies(this IServiceCollection services)
    {


        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // Add OpenAPI with Bearer Authentication Support
        services.AddOpenApi("v1", options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });


        return services;
    }
}
