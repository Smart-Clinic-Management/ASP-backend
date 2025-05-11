namespace SmartClinic.Infrastructure;
public static class DataSeeding
{
    public static async Task SeedAsync(ApplicationDbContext context, UserManager<AppUser> userManager)
    {

        if (!userManager.Users.Any(u => u.UserName == "admin@gmail.com" && u.IsActive))
        {
            AppUser user = new()
            {
                Address = "Farouk St",
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                FirstName = "Hesham",
                LastName = "Elsayed"
            };

            await userManager.CreateAsync(user, "Hesham@1");
            await userManager.AddToRoleAsync(user, "Admin");
        }

    }
}
