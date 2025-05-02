using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartClinic.Infrastructure.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
    {
        const string Admin = "admin";
        const string Doctor = "doctor";
        const string Patient = "patient";


        List<IdentityRole<int>> roles = [
            new(){Id = 1 , Name =Admin , NormalizedName = Admin.ToUpper() },
            new(){Id = 2 , Name =Doctor , NormalizedName = Doctor.ToUpper() },
            new(){Id = 3 , Name =Patient , NormalizedName = Patient.ToUpper() }
            ];

        builder.HasData(roles);


    }
}
