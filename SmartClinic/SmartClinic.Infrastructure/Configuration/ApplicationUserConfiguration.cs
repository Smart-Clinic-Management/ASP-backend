using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartClinic.Domain.Entities;

namespace SmartClinic.Infrastucture.Configuration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasOne(u => u.Doctor)
            .WithOne(d => d.User)
            .HasForeignKey<Doctor>(x => x.UserId);

        builder.HasOne(u => u.Patient)
            .WithOne(d => d.User)
            .HasForeignKey<Patient>(x => x.UserId);
    }
}

