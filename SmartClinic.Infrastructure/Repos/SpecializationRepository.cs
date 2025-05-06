//using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

//namespace SmartClinic.Infrastructure.Repos;

//public class SpecializationRepository(ApplicationDbContext context)
//: GenericRepo<Specialization>(context),
//ISpecializationRepository
//{
//    public Task<int> CountAsync()
//    {
//        throw new NotImplementedException();
//    }

//    public Task<Specialization?> GetByIdAsync(int id)
//        => base.GetSingleAsync(x => x.Id == id && x.IsActive, true,
//             $"{nameof(Specialization.Doctors)}.{nameof(Doctor.User)}",
//                 nameof(Specialization.Appointments));
//    public Task<Specialization?> GetByIdNoTrackingAsync(int id)
//        => base.GetSingleAsync(x => x.Id == id && x.IsActive, false,
//            nameof(Specialization.Doctors));


//    public Task<Specialization?> GetByIdWithIncludesAsync(int id)
//    {
//        return base.GetSingleAsync(x => x.Id == id && x.IsActive, true,
//              $"{nameof(Specialization.Doctors)}.{nameof(Doctor.User)}",
//                 nameof(Specialization.Appointments));

//    }

//    public Task<Specialization?> GetByIdWithIncludesNoTrackingAsync(int id)
//    {
//        return base.GetSingleAsync(x => x.Id == id && x.IsActive, false,
//              nameof(Specialization.Doctors), nameof(Specialization.Appointments));

//    }

//    public Task<IEnumerable<Specialization>> ListAsync(int pageSize = 20, int pageIndex = 1)
//        => base.ListAllAsync(x => x.IsActive, pageSize,
//                  pageIndex, true,
//               nameof(Specialization.Doctors));
//    public Task<IEnumerable<Specialization>> ListNoTrackingAsync(int pageSize = 20, int pageIndex = 1)
//        => base.ListAllAsync(x => x.IsActive, pageSize,
//          pageIndex, false,
//          $"{nameof(Specialization.Doctors)}.{nameof(Doctor.User)}");

//    public Task<IEnumerable<TDto>> ListNoTrackingAsync<TDto>(int pageSize = 20, int pageIndex = 1, string? orderBy = null, bool descending = false, bool isDistinct = false)
//    {
//        throw new NotImplementedException();
//    }
//}
