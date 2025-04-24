using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartClinic.Domain.Entities.AppointmentAggregation;

namespace SmartClinic.Infrastucture.Configuration;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{

    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.OwnsOne(a => a.Duration,
            d =>
            {
                d.Property(x => x.StartTime).HasColumnName("StartTime");
                d.Property(x => x.EndTime).HasColumnName("EndTime");
            });

        builder.Property(a => a.Status)
            .HasConversion(s => s.ToString(),
            db => Enum.Parse<AppointmentStatus>(db))
            .HasColumnType("VARCHAR(50)");
    }
}