using SmartClinic.Application.Features.Patients.Query.DTOs;
using SmartClinic.Application.Features.Patients.Query.DTOs.GetPatient;
using SmartClinic.Application.Features.Patients.Query.DTOs.GetPatients;
using SmartClinic.Application.Mapping;

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
        var patients = await _patientRepo.ListAsync(pageSize, pageIndex);
        var response = patients.Select(patient =>
        {
            return patient.ToGetAllPatientsResponse();
        }).ToList();

        return new ResponseHandler().Success(response);
    }



    public async Task<Response<GetPatientByIdResponse>> GetPatientByIdAsync(int patientId)
    {
        var patient = await _patientRepo.GetByIdWithIncludesAsync(patientId);
        if (patient == null)
        {
            return new ResponseHandler().NotFound<GetPatientByIdResponse>($"No patient found with ID {patientId}");
        }

        var response = patient.ToGetPatientByIdResponse();

        return new ResponseHandler().Success(response);
    }

}
