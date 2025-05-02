using SmartClinic.Infrastructure.Repos;

public class PatientRepo(ApplicationDbContext context) : GenericRepository<Patient>(context), IPatient
{
    public async Task<bool> ExistsAsync(int patientId)
    {
        return await context.Patients.AnyAsync(x => x.Id == patientId && x.IsActive);
    }

    public async Task<Patient?> GetByIdAsync(int id)
    {
        return await context.Patients
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
    }

    public async Task<Patient?> GetByIdNoTrackingAsync(int id)
    {
        return await context.Patients
            .AsNoTracking()
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
    }

    public async Task<Patient?> GetByIdWithIncludesAsync(int id)
    {
        return await context.Patients
            .Include(p => p.User)
            .Include(p => p.Appointments)
            .ThenInclude(a => a.Doctor)
            .ThenInclude(d => d.User)
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
    }

    public async Task<Patient?> GetByIdWithIncludesNoTrackingAsync(int id)
    {
        return await context.Patients
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Appointments)
            .ThenInclude(a => a.Doctor)
            .ThenInclude(d => d.User)
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
    }

    public Task<IEnumerable<Patient>> ListAsync(int pageSize = 20, int pageIndex = 1)
        => base.ListAllAsync(x => x.IsActive, pageSize, pageIndex, true,
            nameof(Patient.User), nameof(Patient.Appointments));

    public Task<IEnumerable<Patient>> ListNoTrackingAsync(int pageSize = 20, int pageIndex = 1)
        => base.ListAllAsync(x => x.IsActive, pageSize, pageIndex, false,
            nameof(Patient.User),
            nameof(Patient.Appointments),
            "Appointments.Doctor",
            "Appointments.Doctor.User",
            "Appointments.Doctor.Specializations");
}
