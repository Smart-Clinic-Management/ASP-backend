using System.Reflection;
using Microsoft.AspNetCore.Identity;

namespace SmartClinic.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<AppUser, IdentityRole<int>, int>(options)
{
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
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {


        #region Doctor
        foreach (var entry in base.ChangeTracker.Entries<Doctor>()
            .Where(x => x.State.Equals(EntityState.Deleted)))
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsActive = false;
            entry.Entity.User.IsActive = false;
            entry.Entity.User.Email = null;
            entry.Entity.DoctorSchedules = [];
            var upcommingAppointment = entry.Entity.Appointments
                .Where(x => x.AppointmentDate >= DateOnly.FromDateTime(DateTime.Now));

            foreach (var appointment in upcommingAppointment)
                appointment.Status = AppointmentStatus.Canceled;

        }
        #endregion

        #region Patient
        foreach (var entry in base.ChangeTracker.Entries<Patient>()
            .Where(x => x.State.Equals(EntityState.Deleted)))
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsActive = false;
            entry.Entity.User.IsActive = false;
            entry.Entity.User.Email = null;
        }
        #endregion

        #region Specialization
        foreach (var entry in base.ChangeTracker.Entries<Specialization>()
            .Where(x => x.State.Equals(EntityState.Deleted)))
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsActive = false;
        }
        #endregion



        return base.SaveChangesAsync(cancellationToken);
    }

}
