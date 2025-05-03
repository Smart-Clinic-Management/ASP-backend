using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartClinic.Infrastructure.Configuration;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasIndex(x => x.IsActive);

        builder.HasOne(d => d.Specialization)
               .WithMany(s => s.Doctors)
               .HasForeignKey(d => d.SpecializationId);

        builder.HasMany(x => x.DoctorSchedules)
            .WithOne(x => x.Doctor)
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Appointments)
            .WithOne(x => x.Doctor)
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
