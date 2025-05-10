namespace SmartClinic.Application.Services.Implementation;
public class PatientService(
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor,
    IPagedCreator<Patient> pagedCreator) : ResponseHandler, IPatientService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IPagedCreator<Patient> _pagedCreator = pagedCreator;

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
