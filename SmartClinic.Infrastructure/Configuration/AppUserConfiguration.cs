using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartClinic.Infrastructure.Configuration;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasIndex(x => x.IsActive);

        builder.HasOne(u => u.Doctor)
            .WithOne(d => d.User)
            .HasForeignKey<Doctor>(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(u => u.Patient)
            .WithOne(d => d.User)
            .HasForeignKey<Patient>(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

