namespace SmartClinic.API;
public static class ModuleAPIDependencies
{
    public static IServiceCollection AddAPIDependencies(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddIdentity<AppUser, IdentityRole<int>>(opts =>
         {
             // password, lockout, etc.
             opts.Password.RequiredLength = 6;
             opts.User.RequireUniqueEmail = true;
         }) // custom DbContext
            .AddEntityFrameworkStores<ApplicationDbContext>();




        // adding auth services

        services.AddScoped<UserManager<AppUser>>();
        services.AddScoped<SignInManager<AppUser>>();
        services.AddScoped<RoleManager<IdentityRole<int>>>();


        // Add OpenAPI with Bearer Authentication Support
        services.AddOpenApi("v1", options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });


        services.AddAuthorizationBuilder()
            .SetDefaultPolicy(new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());

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

        #region Jwt Configuration
        var key = Encoding.ASCII.GetBytes(configuration["ApiSettings:Secret"] ?? throw new Exception("secret not found"));
        services.AddAuthentication(options =>
        {
            options.RequireAuthenticatedSignIn = true;
            options.DefaultAuthenticateScheme =
            options.DefaultSignInScheme =
            options.DefaultSignOutScheme =
            options.DefaultChallengeScheme =
            options.DefaultForbidScheme =
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.FromDays(7),
                ValidateLifetime = true,
            };
        });

        #endregion

        //fluent validation configuration

        #region Fluent Validation
        services.Configure<ApiBehaviorOptions>(options =>
                 options.InvalidModelStateResponseFactory = context =>
                       {
                           var errors = context.ModelState
                               .Where(x => x.Value!.Errors.Count > 0)
                               .SelectMany(x => x.Value!.Errors)
                               .Select(x => x.ErrorMessage)
                               .ToList();

                           var response = new ResponseHandler().BadRequest<string>(errors: errors);

                           return new BadRequestObjectResult(response);
                       }
                       );

        #endregion
        return services;
    }
}
