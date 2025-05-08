using SmartClinic.Application.Features.Specializations.Command.CreateSpecialization;
using SmartClinic.Application.Features.Specializations.Command.DTOs.CreateSpecialization;

namespace SmartClinic.Application.Services.Implementation;

public class DoctorService(
    IUnitOfWork unitOfWork,
    UserManager<AppUser> userManager,
    IFileHandlerService fileHandler,
    IHttpContextAccessor httpContextAccessor,
    IPagedCreator<Doctor> pagedCreator
        ) : ResponseHandler, IDoctorService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IFileHandlerService _fileHandler = fileHandler;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IPagedCreator<Doctor> _pagedCreator = pagedCreator;

    public async Task<Response<GetDoctorByIdResponse?>> GetDoctorByIdAsync(int doctorId)
    {
        var specs = new DoctorByIdSpecification(doctorId, _httpContextAccessor);

        var doctor = await _unitOfWork.Repo<Doctor>()
            .GetEntityWithSpecAsync(specs);

        if (doctor is null)
            return NotFound<GetDoctorByIdResponse?>($"No Doctor With id : {doctorId}");

        return Success(doctor)!;

    }

    public async Task<Response<Pagination<GetAllDoctorsResponse>>> GetAllDoctorsAsync(GetAllDoctorsParams allDoctorsParams)
    {
        var validator = new GetAllDoctorsValidator();

        var validationResult = await validator.ValidateAsync(allDoctorsParams);

        if (!validationResult.IsValid)
            return BadRequest<Pagination<GetAllDoctorsResponse>>(errors: [.. validationResult.Errors.Select(x => x.ErrorMessage)]);

        var specs = new DoctorSpecification(allDoctorsParams, _httpContextAccessor);

        var result = await _pagedCreator
            .CreatePagedResult(_unitOfWork.Repo<Doctor>(), specs, allDoctorsParams.PageIndex, allDoctorsParams.PageSize);

        return Success(result);
    }


    public async Task<Response<string>> CreateDoctor(CreateDoctorRequest newDoctorUser)
    {
        #region Validation
        var validotor = new CreateDoctorValidator(_unitOfWork, _userManager);

        var validationResult = await validotor.ValidateAsync(newDoctorUser);

        if (!validationResult.IsValid)
            return BadRequest<string>(errors: [.. validationResult.Errors.Select(x => x.ErrorMessage)]);
        #endregion

        #region Create User

        var newUser = newDoctorUser.ToUser();

        var createdUser = await _userManager.CreateAsync(newUser, newDoctorUser.Password);

        if (!createdUser.Succeeded)
            return BadRequest<string>("Something went wrong while Creating",
                [.. createdUser.Errors.Select(x => x.Description)]);

        #endregion

        #region Adding Role

        var addingRole = await _userManager.AddToRoleAsync(newUser, "doctor");
        if (!addingRole.Succeeded)
            return BadRequest<string>("Something went wrong while adding to role", [.. createdUser.Errors.Select(x => x.Description)]);

        #region Saving Image

        await _fileHandler
            .SaveFile(newDoctorUser.Image, newDoctorUser.Image.ToFullFilePath(newUser.ProfileImage!));

        #endregion

        #endregion
        return Created("");
    }

    public async Task<Response<UpdateDoctorResponse>> UpdateDoctorAsync(int doctorId, UpdateDoctorRequest request)
    {
        #region Validation
        var validator = new UpdateDoctorRequestValidator();

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest<UpdateDoctorResponse>(errors: [.. validationResult.Errors.Select(s => s.ErrorMessage)]);

        #endregion


        #region Updating

        var specs = new UpdateDoctorSpecification(doctorId);

        var doctor = await _unitOfWork.Repo<Doctor>()
            .GetEntityWithSpecAsync(specs);

        var oldImg = doctor!.User.ProfileImage;

        doctor.UpdateEntity(request);

        #endregion

        if (await _unitOfWork.SaveChangesAsync())
        {
            if (request.Image is not null)
                await _fileHandler.UpdateImg(request.Image, doctor.User.ProfileImage!, oldImg);

            return Success(doctor.ToUpdateDto(_fileHandler), "Updated Successfully");
        }

        return BadRequest<UpdateDoctorResponse>("No changes made");


    }

    public async Task<Response<string>> DeleteById(int id)
    {
        var specs = new DeleteDoctorSpecification(id);

        var doctor = await _unitOfWork.Repo<Doctor>().GetEntityWithSpecAsync(specs);

        if (doctor is null)
            return NotFound<string>();

        doctor.Delete();

        #region Removing doctor schedules

        foreach (var schedule in doctor.DoctorSchedules)
            _unitOfWork.Repo<DoctorSchedule>().Delete(schedule);

        #endregion


        if (!await _unitOfWork.SaveChangesAsync())
            return BadRequest<string>("Something went wrong while deleting");

        return Deleted<string>();
    }


   


    //public async Task<Response<GetDoctorWithAvailableAppointment>> GetDoctorWithAvailableSchedule(int id,
    //    DateOnly startDate)
    //{
    //    var doctor = await _unitOfWork.Repository<IDoctorRepository>()
    //        .GetWithAppointmentsAsync(id, startDate);

    //    if (doctor is null) return NotFound<GetDoctorWithAvailableAppointment>($"No Doctor with id : {id}");

    //    // get requested dates

    //    HashSet<DayOfWeek> requestedDates = GetRequestedDates(startDate);

    //    List<DayOfWeek> DoctorSchedulesDays = [.. doctor!.DoctorSchedules.Select(x => x.DayOfWeek)];

    //    List<AvailableSchedule> AvailableSchedules = AddingSchudlesDays(requestedDates, DoctorSchedulesDays);

    //    // create the slots

    //    AvailableSchedules = AddingDaySlots(doctor, AvailableSchedules);

    //    doctor.User.ProfileImage = _fileHandler.GetFileURL(doctor.User.ProfileImage!);

    //    var result = doctor.ToGetDoctorWithAvailableSchedules(AvailableSchedules);
    //    return Success(result, "Found");
    //}

    //private static HashSet<DayOfWeek> GetRequestedDates(DateOnly startDate)
    //{
    //    HashSet<DayOfWeek> requestedDates = [];

    //    for (int i = 0; i < 3; i++)
    //        requestedDates.Add(startDate.AddDays(i).DayOfWeek);
    //    return requestedDates;
    //}

    //private static List<AvailableSchedule> AddingSchudlesDays(HashSet<DayOfWeek> requestedDates, List<DayOfWeek> DoctorSchedulesDays)
    //{
    //    List<AvailableSchedule> AvailableDays = [];

    //    foreach (var day in requestedDates)
    //    {
    //        if (DoctorSchedulesDays.Contains(day))
    //            AvailableDays.Add(new() { Day = day.ToString()!, IsAvailable = true });
    //        else
    //            AvailableDays.Add(new() { Day = day.ToString()! });
    //    }

    //    return AvailableDays;
    //}



    //private static List<AvailableSchedule> AddingDaySlots(Doctor doctor, List<AvailableSchedule> AvailableDays)
    //{
    //    List<AvailableSchedule> newAvailableDays = AvailableDays;

    //    List<TimeOnly> reservedSlots = [.. doctor.Appointments.Select(x => x.Duration.StartTime)];

    //    foreach (var day in newAvailableDays.Where(x => x.IsAvailable))
    //    {

    //        var scheduleDay = doctor.DoctorSchedules
    //            .FirstOrDefault(x => x.DayOfWeek.ToString().Equals(day.Day))!;

    //        var dayAppointments = doctor.Appointments
    //            .Where(x => x.AppointmentDate.DayOfWeek.ToString().Equals(day.Day))
    //            .Select(x => x.Duration.StartTime).ToList();

    //        while (scheduleDay.StartTime < scheduleDay.EndTime)
    //        {
    //            if (!dayAppointments.Contains(scheduleDay.StartTime))
    //                day.Slots.Add(new(scheduleDay.StartTime, true));
    //            else
    //                day.Slots.Add(new(scheduleDay.StartTime, false));

    //            scheduleDay.StartTime = scheduleDay.StartTime.AddMinutes(scheduleDay.SlotDuration);
    //        }

    //    }

    //    return newAvailableDays;
    //}


}


