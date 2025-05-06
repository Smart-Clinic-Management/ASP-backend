//using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

//namespace SmartClinic.Infrastructure.Repos;

//public class DoctorScheduleRepository : GenericRepo<DoctorSchedule>, IDoctorSchedule
//{
//    private readonly ApplicationDbContext _context;

//    public DoctorScheduleRepository(ApplicationDbContext context)
//        : base(context)
//    {
//        _context = context;
//    }

//    public async Task<DoctorSchedule?> GetByIdAsync(int id)
//    {
//        return await GetSingleAsync(ds => ds.Id == id, true,
//            nameof(DoctorSchedule.Doctor),
//            "Doctor.User");
//    }

//    public async Task<DoctorSchedule?> GetByIdNoTrackingAsync(int id)
//    {
//        return await GetSingleAsync(ds => ds.Id == id, false);
//    }

//    public async Task<IEnumerable<DoctorSchedule>> GetByDoctorIdAsync(int doctorId)
//    {
//        var schedules = await _context.DoctorSchedules
//            .Where(ds => ds.DoctorId == doctorId)
//            .Include(ds => ds.Doctor)
//            .ThenInclude(d => d.User)
//            .ToListAsync();

//        if (schedules == null || !schedules.Any())
//        {
//            throw new KeyNotFoundException($"No schedules found for doctor with ID {doctorId}");
//        }

//        return schedules;
//    }

//    public async Task<DoctorSchedule?> GetByDoctorAndDayAsync(int doctorId, DayOfWeek day)
//    {
//        return await _context.DoctorSchedules
//            .FirstOrDefaultAsync(ds => ds.DoctorId == doctorId && ds.DayOfWeek == day);
//    }

//    public async Task<bool> SoftDeleteAsync(int id)
//    {
//        var entity = await _context.DoctorSchedules.FindAsync(id);
//        if (entity == null)
//            return false;

//        _context.DoctorSchedules.Remove(entity);
//        await _context.SaveChangesAsync();
//        return true;
//    }

//    public Task<IEnumerable<DoctorSchedule>> ListAsync(int pageSize = 20, int pageIndex = 1)
//    {
//        return ListAllAsync(null, pageSize, pageIndex, true,
//            nameof(DoctorSchedule.Doctor),
//            "Doctor.User");
//    }

//    public Task<IEnumerable<DoctorSchedule>> ListNoTrackingAsync(int pageSize = 20, int pageIndex = 1)
//    {
//        return ListAllAsync(null, pageSize, pageIndex, false,
//            nameof(DoctorSchedule.Doctor),
//            "Doctor.User");
//    }

//    public Task<DoctorSchedule?> GetByIdWithIncludesAsync(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<DoctorSchedule?> GetByIdWithIncludesNoTrackingAsync(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IEnumerable<TDto>> ListNoTrackingAsync<TDto>(int pageSize = 20, int pageIndex = 1, string? orderBy = null, bool descending = false, bool isDistinct = false)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<int> CountAsync()
//    {
//        throw new NotImplementedException();
//    }
//}
