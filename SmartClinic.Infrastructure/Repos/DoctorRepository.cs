namespace SmartClinic.Infrastructure.Repos;
public class DoctorRepository(ApplicationDbContext context)
    : GenericRepository<Doctor>(context),
    IDoctorRepository
{
    public Task<bool> ExistsAsync(int doctorId)
        => context.Doctors.AnyAsync(x => x.Id == doctorId && x.IsActive);

    public Task<Doctor?> GetByIdAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive,
            includes: nameof(Doctor.User));

    public Task<Doctor?> GetByIdNoTrackingAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive, false,
            nameof(Doctor.User));

    public Task<Doctor?> GetByIdWithIncludesAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive, true,
          nameof(Doctor.User), nameof(Doctor.Specialization), nameof(Doctor.DoctorSchedules));

    public Task<Doctor?> GetByIdWithIncludesNoTrackingAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive, false,
              nameof(Doctor.User), nameof(Doctor.Specialization));

    public async Task<Doctor?> GetDoctorWithSpecificScheduleAsync(int doctorId, DateOnly appointmentDate, TimeOnly startTime, int timeSlot)
    {
        return await context.Doctors.AsNoTracking()
                .Where(x => x.Id == doctorId && x.IsActive)
                .Include(x => x.DoctorSchedules
                            .Where(s => s.DayOfWeek == appointmentDate.DayOfWeek &&
                            s.SlotDuration == timeSlot &&
                             s.StartTime <= startTime &&
                             s.EndTime >= startTime.AddMinutes(s.SlotDuration)))
                .Include(x => x.Appointments
                            .Where(a => a.AppointmentDate == appointmentDate &&
                            a.Duration.StartTime <= startTime &&
                            a.Duration.EndTime > startTime))
                .FirstOrDefaultAsync();
    }

    public async Task<Doctor?> GetWithAppointmentsAsync(int id, DateOnly startDate)
    {
        var endDate = startDate.AddDays(3);

        return await context.Doctors.AsNoTracking()
            .Where(x => x.Id == id && x.IsActive)
            .Include(x => x.Specialization)
            .Include(x => x.DoctorSchedules)
            .Include(x => x.User)
            .Include(x => x.Appointments
            .Where(a => a.AppointmentDate >= startDate &&
                                 a.AppointmentDate < endDate))
            .FirstOrDefaultAsync();
    }


    public async Task<bool> IsValidDoctorSpecialization(int specializationId, int doctorId)
        => await context.Doctors.AsNoTracking().AnyAsync(x => x.Id == doctorId &&
                  x.SpecializationId == specializationId && x.IsActive);

    public Task<IEnumerable<Doctor>> ListAsync(int pageSize = 20, int pageIndex = 1)
        => base.ListAllAsync(x => x.IsActive, pageSize,
                  pageIndex, true,
               nameof(Doctor.User), nameof(Doctor.Specialization));

    public Task<IEnumerable<Doctor>> ListNoTrackingAsync(int pageSize = 20, int pageIndex = 1)
        => base.ListAllAsync(x => x.IsActive, pageSize,
                  pageIndex, false,
               nameof(Doctor.User), nameof(Doctor.Specialization));




}