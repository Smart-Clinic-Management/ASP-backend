using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartClinic.Domain.Entities;

namespace SmartClinic.Infrastucture.Configuration;

public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
    {
        builder.Property(ds => ds.DayOfWeek)
            .HasConversion(s => s.ToString(),
            db => Enum.Parse<DaysOfWeek>(db))
            .HasColumnType("VARCHAR(50)");


    }
}
