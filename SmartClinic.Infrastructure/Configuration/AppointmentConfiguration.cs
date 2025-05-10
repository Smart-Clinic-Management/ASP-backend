namespace SmartClinic.Infrastructure.Configuration;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{

    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.OwnsOne(a => a.Duration,
            d =>
            {
                d.Property(x => x.StartTime).HasColumnName("StartTime");
                d.Property(x => x.EndTime).HasColumnName("EndTime");
                d.HasIndex(x => x.StartTime);
            });


        builder.Property(a => a.Status)
            .HasConversion(s => s.ToString(),
            db => Enum.Parse<AppointmentStatus>(db))
            .HasColumnType("VARCHAR(50)");


        builder.HasOne(x => x.Specialization)
            .WithMany(x => x.Appointments)
            .HasForeignKey(x => x.SpecializationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}