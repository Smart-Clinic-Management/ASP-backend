using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAcess.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            Dictionary<string, string> roles = new()
            {
                { "43d0590f-2f82-4867-83c4-18f0488f9706", "admin" },
                { "ff715d53-7725-48de-8d74-f064b8b41b45", "doctor" },
                { "5654533a-52b5-4e1e-b9e5-fd8036ef35ff", "patient" },
            };

            foreach (var role in roles)
            {
                builder.HasData(new IdentityRole
                {
                    Id = role.Key,
                    Name = role.Value,
                    NormalizedName = role.Value.ToUpper()
                });
            }
        }
    }
}
