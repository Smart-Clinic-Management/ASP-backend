using SmartClinic.Application.Services.Implementation.Specifications.PatientSpecifications.GetPatients;

public class PatientService : ResponseHandler, IPatientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IFileHandlerService _fileHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPagedCreator<Patient> _pagedCreator; 

    public PatientService(
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager,
        IFileHandlerService fileHandler,
        IHttpContextAccessor httpContextAccessor,
        IPagedCreator<Patient> pagedCreator)  
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _fileHandler = fileHandler;
        _httpContextAccessor = httpContextAccessor;
        _pagedCreator = pagedCreator;  
    }

    public async Task<Response<Pagination<GetAllPatientsResponse>>> GetAllPatientsAsync(GetAllPatientsParams allPatientsParams)
    {
        var validator = new GetAllPatientsValidator();
        var validationResult = await validator.ValidateAsync(allPatientsParams);

        if (!validationResult.IsValid)
            return BadRequest<Pagination<GetAllPatientsResponse>>(errors: validationResult.Errors.Select(x => x.ErrorMessage).ToList());

        var specs = new PatientSpecification(allPatientsParams, _httpContextAccessor);

        var result = await _pagedCreator
            .CreatePagedResult(_unitOfWork.Repo<Patient>(), specs, allPatientsParams.PageIndex, allPatientsParams.PageSize);

        return Success(result);
    }
}
