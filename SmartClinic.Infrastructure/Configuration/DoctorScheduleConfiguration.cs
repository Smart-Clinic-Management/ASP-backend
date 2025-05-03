using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartClinic.Infrastructure.Configuration;

public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
    {
        builder.HasIndex(x => x.StartTime);
        builder.HasIndex(x => x.DayOfWeek);

        builder.Property(ds => ds.DayOfWeek)
            .HasConversion(s => s.ToString(),
            db => Enum.Parse<DayOfWeek>(db))
            .HasColumnType("VARCHAR(50)");


    }
}
