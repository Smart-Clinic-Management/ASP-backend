using Microsoft.AspNetCore.Identity;
using SmartClinic.Domain.Entities;
using SmartClinic.Infrastructure.Data;

namespace SmartClinic.API;
public static class ModuleAPIDependencies
{
    public static IServiceCollection AddAPIDependencies(this IServiceCollection services)
    {

        services
     .AddIdentityCore<AppUser>(opts =>
     {
         // password, lockout, etc.
         opts.Password.RequireNonAlphanumeric = true;
         opts.Password.RequireUppercase = false;
         opts.Password.RequiredLength = 6;
     })
     .AddRoles<IdentityRole>()                             // enable roles
     .AddEntityFrameworkStores<ApplicationDbContext>()      // your EF store
     .AddSignInManager();


        // Add OpenAPI with Bearer Authentication Support
        services.AddOpenApi("v1", options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });


        return services;
    }
}
