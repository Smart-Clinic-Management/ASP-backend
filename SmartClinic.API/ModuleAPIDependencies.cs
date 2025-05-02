using Microsoft.AspNetCore.Mvc;
using SmartClinic.Application.Bases;

namespace SmartClinic.API;
public static class ModuleAPIDependencies
{
    public static IServiceCollection AddAPIDependencies(this IServiceCollection services, IConfiguration configuration)
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


        // Configure JWT Authentication instead of cookies

        var key = Encoding.ASCII.GetBytes(configuration["ApiSettings:Secret"] ?? throw new Exception("secret not found"));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.FromDays(7)
            };
        });


        services.Configure<ApiBehaviorOptions>(options =>
                  options.InvalidModelStateResponseFactory = context =>
                        {
                            var errors = context.ModelState
                                .Where(x => x.Value.Errors.Count > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(x => x.ErrorMessage)
                                .ToList();

                            var response = new ResponseHandler().BadRequest<string>(errors);

                            return new BadRequestObjectResult(response);
                        }
                        );

        return services;
    }
}
