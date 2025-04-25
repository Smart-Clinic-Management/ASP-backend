using SmartClinic.Domain.Entities.AppointmentAggregation;
using SmartClinic.Infrastucture.Configuration;

namespace SmartClinic.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected ApplicationDbContext()
    {
    }

    public DbSet<AppUser> ApplicationUser { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<DoctorSchedule> DoctorSchedules { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Apply separate configuration classes
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationUserConfiguration).Assembly);

    }

}
