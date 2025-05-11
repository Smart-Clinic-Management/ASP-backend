var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(op =>
    op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddInfrastructureDependencies()
    .AddAPIDependencies(builder.Configuration)
    .AddApplicationDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(op =>
                op.WithTheme(ScalarTheme.Mars)
                .WithDefaultHttpClient(ScalarTarget.Shell, ScalarClient.Httpie));
}

app.UseStaticFiles();

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();


app.UseCors("AllowAngularApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


// seeding Data

try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        await context.Database.MigrateAsync();
        await DataSeeding.SeedAsync(context, userManager);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}


app.Run();