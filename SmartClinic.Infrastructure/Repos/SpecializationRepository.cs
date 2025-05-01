namespace SmartClinic.Infrastructure.Repos;

public class SpecializationRepository(ApplicationDbContext context)
: GenericRepository<Specialization>(context),
ISpecializaionRepository
{
    public async Task<bool> ExistsAsync(int specializationId)
        => await context.Specializations.AnyAsync(x => x.Id == specializationId && x.IsActive);

    public Task<Specialization?> GetByIdAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive, true,
             $"{nameof(Specialization.Doctors)}.{nameof(Doctor.User)}",
                 nameof(Specialization.Appointments));
    public Task<Specialization?> GetByIdNoTrackingAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive, false,
            nameof(Specialization.Doctors));


    public Task<Specialization?> GetByIdWithIncludesAsync(int id)
    {
        return base.GetSingleAsync(x => x.Id == id && x.IsActive, true,
              $"{nameof(Specialization.Doctors)}.{nameof(Doctor.User)}",
                 nameof(Specialization.Appointments));

    }

    public Task<Specialization?> GetByIdWithIncludesNoTrackingAsync(int id)
    {
        return base.GetSingleAsync(x => x.Id == id && x.IsActive, false,
              nameof(Specialization.Doctors), nameof(Specialization.Appointments));

    }

    public Task<IEnumerable<Specialization>> ListAsync(int pageSize = 20, int pageIndex = 1)
        => base.ListAllAsync(x => x.IsActive, pageSize,
                  pageIndex, true,
               nameof(Specialization.Doctors));
    public Task<IEnumerable<Specialization>> ListNoTrackingAsync(int pageSize = 20, int pageIndex = 1)
        => base.ListAllAsync(x => x.IsActive, pageSize,
          pageIndex, false,
          $"{nameof(Specialization.Doctors)}.{nameof(Doctor.User)}");

}
