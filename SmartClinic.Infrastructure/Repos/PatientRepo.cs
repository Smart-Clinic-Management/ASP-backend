

namespace SmartClinic.Infrastructure.Repos
{
    public class PatientRepo : GenericRepository<Patient>, IPatient
    {
        public PatientRepo(ApplicationDbContext context) : base(context)
        {
        }

        public Task<Patient?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Patient?> GetByIdNoTrackingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Patient?> GetByIdWithIncludesAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Patient?> GetByIdWithIncludesNoTrackingAsync(int id)
        {
            throw new NotImplementedException();
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
}
