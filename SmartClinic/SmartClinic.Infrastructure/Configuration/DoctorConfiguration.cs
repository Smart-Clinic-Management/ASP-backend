using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartClinic.Domain.Entities;

namespace SmartClinic.Infrastucture.Configuration;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasMany(d => d.Specializations)
               .WithMany(s => s.Doctors)
               .UsingEntity("DoctorsSpecializations");

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

