//using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

//namespace SmartClinic.Application.Services.Implementation;

//public class DoctorScheduleServices : ResponseHandler, IDoctorScheduleService
//{
//    private readonly IDoctorSchedule _doctorScheduleRepo;
//    private readonly IUnitOfWork _unitOfWork;

//    public DoctorScheduleServices(IDoctorSchedule doctorScheduleRepo, IUnitOfWork unitOfWork)
//    {
//        _doctorScheduleRepo = doctorScheduleRepo;
//        _unitOfWork = unitOfWork;
//    }

//    //public async Task<Response<IEnumerable<GetDoctorSchedule>>> GetSchedulesForDoctorAsync(int doctorId)
//    //{
//    //    var schedules = await _doctorScheduleRepo.GetByDoctorIdAsync(doctorId);

//    //    if (schedules == null || !schedules.Any())
//    //        return NotFound<IEnumerable<GetDoctorSchedule>>();

//    //    var scheduleDtos = schedules.Select(schedule => schedule.ToGetDoctorScheduleDto()).ToList();

//    //    return Success<IEnumerable<GetDoctorSchedule>>(scheduleDtos);
//    //}

//    //public async Task<Response<DeleteSchedulesResponse>> DeleteScheduleByIdAsync(int scheduleId)
//    //{
//    //    var isDeleted = await _doctorScheduleRepo.SoftDeleteAsync(scheduleId);

//    //    if (!isDeleted)
//    //        return NotFound<DeleteSchedulesResponse>($"No schedule found with ID {scheduleId}.");

//    //    return Deleted<DeleteSchedulesResponse>();
//    //}

//    //public async Task<Response<GetDoctorSchedule>> CreateAsync(CreateDoctorScheduleRequest request)
//    //{
//    //    var schedule = request.ToEntity();

//    //    await _doctorScheduleRepo.AddAsync(schedule);
//    //    await _unitOfWork.SaveChangesAsync();

//    //    return Created(schedule.ToGetDoctorScheduleDto());

//    //}



//    //public async Task<Response<GetDoctorSchedule>> UpdateAsync(UpdateDoctorScheduleRequest request)
//    //{
//    //    var schedule = await _doctorScheduleRepo.GetByIdAsync(request.Id);

//    //    if (schedule is null)
//    //        return BadRequest<GetDoctorSchedule>([$"Schedule with ID {request.Id} not found."]);

//    //    schedule.UpdateFromRequest(request);

//    //    await _unitOfWork.SaveChangesAsync();

//    //    return Success(schedule.ToGetDoctorScheduleDto());

//    //}



//}
