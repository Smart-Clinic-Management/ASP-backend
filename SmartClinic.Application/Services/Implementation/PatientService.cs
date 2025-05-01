using SmartClinic.Application.Features.Patients.Query.DTOs;

namespace SmartClinic.Application.Services.Implementation;

public class PatientService : IPatientService
{
    private readonly IPatient _patientRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IFileHandlerService _fileHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISpecializationService _specializationService;

    public PatientService(
       IPatient patientRepo,
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager,
        IFileHandlerService fileHandler,
        IHttpContextAccessor httpContextAccessor,
        ISpecializationService specializationService)
    {
        _patientRepo = patientRepo;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _fileHandler = fileHandler;
        _httpContextAccessor = httpContextAccessor;
        _specializationService = specializationService;
    }
    public async Task<Response<List<GetAllPatientsResponse>>> GetAllPatientsAsync(int pageSize = 20, int pageIndex = 1)
    {
        var patients = await _patientRepo.ListNoTrackingAsync(pageSize, pageIndex);

        //var invalidAppointments = new List<string>();

        //foreach (var patient in patients)
        //{
        //    foreach (var appointment in patient.Appointments)
        //    {
        //        if (appointment.Doctor == null)
        //        {
        //            invalidAppointments.Add($"Appointment for patient {patient.Id} is missing a Doctor.");
        //        }

        //        if (appointment.Doctor?.Specializations == null || !appointment.Doctor.Specializations.Any())
        //        {
        //            invalidAppointments.Add($"Appointment for patient {patient.Id} is missing a Specialization.");
        //        }

        //    }
        //}

        //if (invalidAppointments.Any())
        //{
        //    return new Response<List<GetAllPatientsResponse>>()
        //    {
        //        Message = string.Join("\n", invalidAppointments)
        //    };
        //}

        var response = patients
            .Select(p => p.ToGetAllPatientsResponse(_httpContextAccessor))
            .ToList();

        return new ResponseHandler().Success(response);
    }




}
