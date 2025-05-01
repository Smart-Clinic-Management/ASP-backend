namespace SmartClinic.Infrastructure.Repos;
public class DoctorRepository(ApplicationDbContext context)
    : GenericRepository<Doctor>(context),
    IDoctorRepository
{
    public Task<Doctor?> GetByIdAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive,
            includes: nameof(Doctor.User));

    public Task<Doctor?> GetByIdNoTrackingAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive, false,
            nameof(Doctor.User));

    public Task<Doctor?> GetByIdWithIncludesAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive, true,
          nameof(Doctor.User), nameof(Doctor.Specializations), nameof(Doctor.DoctorSchedules));

    public Task<Doctor?> GetByIdWithIncludesNoTrackingAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive, false,
              nameof(Doctor.User), nameof(Doctor.Specializations));

    public async Task<Doctor?> GetWithAppointmentsAsync(int id, DateOnly startDate)
    {
        var endDate = startDate.AddDays(3);

        return await context.Doctors.AsNoTracking()
            .Where(x => x.Id == id && x.IsActive)
            .Include(x => x.Specializations)
            .Include(x => x.DoctorSchedules)
            .Include(x => x.User)
            .Include(x => x.Appointments
            .Where(a => a.AppointmentDate >= startDate &&
                                 a.AppointmentDate < endDate))
            .FirstOrDefaultAsync();
    }

    public Task<IEnumerable<Doctor>> ListAsync(int pageSize = 20, int pageIndex = 1)
        => base.ListAllAsync(x => x.IsActive, pageSize,
                  pageIndex, true,
               nameof(Doctor.User), nameof(Doctor.Specializations));

    public Task<IEnumerable<Doctor>> ListNoTrackingAsync(int pageSize = 20, int pageIndex = 1)
        => base.ListAllAsync(x => x.IsActive, pageSize,
                  pageIndex, false,
               nameof(Doctor.User), nameof(Doctor.Specializations));




}