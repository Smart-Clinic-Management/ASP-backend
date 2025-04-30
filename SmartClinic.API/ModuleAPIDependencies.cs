namespace SmartClinic.API;
public static class ModuleAPIDependencies
{
    public static IServiceCollection AddAPIDependencies(this IServiceCollection services)
    {

        services
     .AddIdentityCore<AppUser>(opts =>
     {
         // password, lockout, etc.
         opts.Password.RequireNonAlphanumeric = false;
         opts.Password.RequireUppercase = false;
         opts.Password.RequiredLength = 6;
         opts.User.RequireUniqueEmail = true;
     })
     .AddRoles<IdentityRole>()                             // enable roles
     .AddEntityFrameworkStores<ApplicationDbContext>()      // your EF store
     .AddSignInManager();


        // Add OpenAPI with Bearer Authentication Support
        services.AddOpenApi("v1", options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });


        services.AddScoped<SignInManager<AppUser>>();


        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });

        // ? Add CORS policy
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        return services;
    }
}
