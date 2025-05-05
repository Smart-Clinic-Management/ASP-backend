using SmartClinic.Application.Services.Implementation.Specifications;
using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

namespace SmartClinic.Application.Services.Implementation;

public class DoctorService : ResponseHandler, IDoctorService
{
    private readonly IDoctorRepository _doctorRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IFileHandlerService _fileHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISpecializationService _specializationService;
    private readonly IPagedCreator<Doctor> _pagedCreator;
    private readonly ISpecializationRepository _specialRepo;


    public DoctorService(
        IDoctorRepository doctorRepo,
        ISpecializationRepository specialRepo,
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager,
        IFileHandlerService fileHandler,
        IHttpContextAccessor httpContextAccessor,
        ISpecializationService specializationService,
        IPagedCreator<Doctor> pagedCreator
        )
    {
        _doctorRepo = doctorRepo;
        _specialRepo = specialRepo;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _fileHandler = fileHandler;
        _httpContextAccessor = httpContextAccessor;
        _specializationService = specializationService;
        this._pagedCreator = pagedCreator;
    }

    public async Task<Response<GetDoctorByIdResponse>> GetDoctorByIdAsync(int doctorId)
    {
        var doctor = await _doctorRepo.GetByIdWithIncludesAsync(doctorId);
        if (doctor == null)
            return new ResponseHandler().NotFound<GetDoctorByIdResponse>($"No doctor found with ID {doctorId}");

        var imageUrl = DoctorMappingExtensions.GetImgUrl(doctor.User.ProfileImage, _httpContextAccessor);

        var response = doctor.ToGetDoctorByIdResponse();
        response = response with { image = imageUrl };

        return new ResponseHandler().Success(response);
    }

    public async Task<Response<Pagination<GetAllDoctorsResponse>>> GetAllDoctorsAsync(GetAllDoctorsParams allDoctorsParams)
    {
        var repo = _unitOfWork.Repository<IDoctorRepository>();

        var specs = new DoctorSpecifications(allDoctorsParams);

        var doctors = await repo
            .ListNoTrackingAsync(allDoctorsParams, specs);

        var TotalCount = await repo.CountAsync(specs);

        var reslult = _pagedCreator
             .CreatePagedResult([.. doctors], allDoctorsParams.PageIndex,
             allDoctorsParams.PageSize, TotalCount);

        return Success(reslult);
    }

    public async Task<Response<SoftDeleteDoctorResponse>> SoftDeleteDoctorAsync(int doctorId)
    {
        var doctor = await _doctorRepo.GetByIdAsync(doctorId);
        if (doctor == null)
        {
            return new ResponseHandler().NotFound<SoftDeleteDoctorResponse>($"No doctor found with ID {doctorId}");
        }

        doctor.IsActive = false;
        _doctorRepo.Update(doctor);

        var user = await _userManager.FindByIdAsync(doctor.Id.ToString());
        if (user != null)
        {
            user.IsActive = false;
            await _userManager.UpdateAsync(user);
        }

        await _unitOfWork.SaveChangesAsync();

        return new ResponseHandler().Success(new SoftDeleteDoctorResponse("Doctor and associated user successfully soft deleted."));
    }

    //public async Task<Response<CreateDoctorResponse>> CreateDoctor(CreateDoctorRequest newDoctorUser)
    //{
    //    if (newDoctorUser.Image == null)
    //    {
    //        return new ResponseHandler().BadRequest<CreateDoctorResponse>(errors: ["No image uploaded"]);
    //    }

    //    var validationOptions = new FileValidation
    //    {
    //        MaxSize = 2 * 1024 * 1024,
    //        AllowedExtenstions = [".jpg", ".jpeg", ".png"]
    //    };

    //    var fileResult = await _fileHandler.HanldeFile(newDoctorUser.Image, validationOptions);

    //    if (!fileResult.Success)
    //    {
    //        var errors = new List<string>();
    //        if (!string.IsNullOrEmpty(fileResult.Error)) errors.Add(fileResult.Error);
    //        return new ResponseHandler().BadRequest<CreateDoctorResponse>(errors: errors);
    //    }

    //    var user = new AppUser
    //    {
    //        UserName = newDoctorUser.Email,
    //        Email = newDoctorUser.Email,
    //        FirstName = newDoctorUser.Fname,
    //        LastName = newDoctorUser.Lname,
    //        Address = newDoctorUser.Address,
    //        BirthDate = newDoctorUser.BirthDate,
    //        ProfileImage = fileResult.RelativeFilePath,
    //    };

    //    var userCreationResult = await _userManager.CreateAsync(user, "DefaultPassword123");

    //    if (!userCreationResult.Succeeded)
    //    {
    //        var errors = userCreationResult.Errors.Select(e => e.Description).ToList();
    //        return new ResponseHandler().BadRequest<CreateDoctorResponse>(errors);
    //    }

    //    await _userManager.AddToRoleAsync(user, "Doctor");

    //    var doctor = new Doctor
    //    {
    //        Id = user.Id,
    //        Description = newDoctorUser.Description,
    //        WaitingTime = newDoctorUser.WaitingTime,
    //        IsActive = true
    //    };

    //    await doctor.AddSpecializationsToDoctorAsync(_specialRepo, newDoctorUser.SpecializationId);

    //    await _doctorRepo.AddAsync(doctor);

    //    if (fileResult.Success)
    //    {
    //        await _fileHandler.SaveFile(newDoctorUser.Image, fileResult.FullFilePath);
    //    }

    //    await _unitOfWork.SaveChangesAsync();

    //    var response = new CreateDoctorResponse(
    //        Fname: user.FirstName,
    //        Lname: user.LastName,
    //        Email: user.Email,
    //        Image: fileResult.Success ? DoctorMappingExtensions.GetImgUrl(fileResult.RelativeFilePath, _httpContextAccessor) : null,
    //        SpecializationId: doctor.Specialization.Id,
    //        BirthDate: user.BirthDate,
    //        Address: user.Address,
    //        WaitingTime: doctor.WaitingTime,
    //        Description: doctor.Description
    //    );

    //    return new ResponseHandler().Success(response, message: "Doctor Created Successfully");
    //}

    //public async Task<Response<UpdateDoctorResponse>> UpdateDoctorAsync(int doctorId, UpdateDoctorRequest request)
    //{
    //    var doctor = await _doctorRepo.GetByIdWithIncludesAsync(doctorId);
    //    if (doctor == null || !doctor.IsActive)
    //    {
    //        return new ResponseHandler().NotFound<UpdateDoctorResponse>($"No Doctor found with id {doctorId}");
    //    }

    //    var user = doctor.User;
    //    user.FirstName = request.Fname ?? user.FirstName;
    //    user.LastName = request.Lname ?? user.LastName;
    //    user.Email = request.Email ?? user.Email;
    //    user.Address = request.Address ?? user.Address;

    //    if (request.Image != null)
    //    {
    //        var validationOptions = new FileValidation
    //        {
    //            MaxSize = 2 * 1024 * 1024,
    //            AllowedExtenstions = [".jpg", ".jpeg", ".png"]
    //        };

    //        var fileResult = await _fileHandler.HanldeFile(request.Image, validationOptions);

    //        if (!fileResult.Success)
    //        {
    //            var errors = new List<string> { fileResult.Error };
    //            return new ResponseHandler().BadRequest<UpdateDoctorResponse>(errors);
    //        }

    //        user.ProfileImage = fileResult.RelativeFilePath;
    //        await _fileHandler.SaveFile(request.Image, fileResult.FullFilePath);
    //    }

    //    await doctor.AddSpecializationsToDoctorAsync(_specialRepo, request.SpecializationId);

    //    await _unitOfWork.SaveChangesAsync();

    //    var imageUrl = DoctorMappingExtensions.GetImgUrl(user.ProfileImage, _httpContextAccessor);

    //    var response = doctor.ToUpdateDoctorResponse(_httpContextAccessor) with { Image = imageUrl };

    //    return new ResponseHandler().Success(response, message: "Doctor updated successfully");
    //}

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


