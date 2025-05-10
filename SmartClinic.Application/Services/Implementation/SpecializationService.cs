namespace SmartClinic.Application.Services.Implementation;

public class SpecializationService(
IFileHandlerService fileHandler,
IHttpContextAccessor httpContextAccessor,
IUnitOfWork unitOfWork,
 IPagedCreator<Specialization> pagedCreator,
UserManager<AppUser> userManager)
    : ResponseHandler, ISpecializationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileHandlerService _fileHandler = fileHandler;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IPagedCreator<Specialization> _pagedCreator = pagedCreator;

    public async Task<Response<Pagination<GetAllSpecializationsResponse>>> GetAllSpecializationsAsync(GetAllSpecializationsParams allSpecializationsParams)
    {
        var validator = new GetAllSpecializationsValidator();

        var validateResult = await validator.ValidateAsync(allSpecializationsParams);

        if (!validateResult.IsValid)
        {
            return BadRequest<Pagination<GetAllSpecializationsResponse>>(errors: [.. validateResult.Errors.Select(x => x.ErrorMessage)]);
        }
        var specs = new SpecializationsSpecification(allSpecializationsParams, _httpContextAccessor);

        var result = await _pagedCreator
      .CreatePagedResult(_unitOfWork.Repo<Specialization>(), specs, allSpecializationsParams.PageIndex, allSpecializationsParams.PageSize);

        return Success(result);

    }

    public async Task<Response<GetSpecializationByIdResponse?>> GetSpecializationByIdAsync(int SpecializationId)
    {
        var specs = new SpecializationByIdSpecification(SpecializationId, _httpContextAccessor);
        var Specialization = await _unitOfWork.Repo<Specialization>().GetEntityWithSpecAsync(specs);
        if (Specialization is null)
            return NotFound<GetSpecializationByIdResponse?>($"No Specialization with id : {SpecializationId}");

        return Success(Specialization)!;
    }




    public async Task<Response<string>> CreateSpecialization(CreateSpecializationRequest newSpecializationUser)
    {
        #region Validation
        var validator = new CreateSpecializationValidator(_unitOfWork);

        var validationResult = await validator.ValidateAsync(newSpecializationUser);

        if (!validationResult.IsValid)
            return BadRequest<string>(errors: validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        #endregion

        #region Create Specialization

        var specialization = newSpecializationUser.ToSpecialization();

        await _unitOfWork.Repo<Specialization>().AddAsync(specialization);
        #endregion

        #region Saving Image

        if (newSpecializationUser.Image != null)
        {
            await _fileHandler
                .SaveFile(newSpecializationUser.Image, newSpecializationUser.Image.ToFullFilePath(specialization.Image));
        }

        #endregion

        return Created("Specialization created successfully");
    }

    public async Task<Response<string>> DeleteSpecializationAsync(int specializationId)
    {
        var specs = new DeleteSpecializationSpecification(specializationId);

        var specialization = await _unitOfWork.Repo<Specialization>().GetEntityWithSpecAsync(specs);

        if (specialization is null)
            return NotFound<string>($"No specialization with id {specializationId}");

        if (specialization.Doctors.Any())
            return BadRequest<string>("Can't Delete Specialization that have doctors assigned");

        specialization.IsActive = false;

        await _unitOfWork.SaveChangesAsync();

        return Success("", "Deleted Successfully");
    }


    public async Task<Response<UpdateSpecializationResponse>> UpdateSpecializationAsync(int specializationId, UpdateSpecializationRequest updatedSpecialization)
    {
        #region Validation
        var validator = new UpdateSpecializationValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(updatedSpecialization);

        if (!validationResult.IsValid)
            return BadRequest<UpdateSpecializationResponse>(errors: validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        #endregion

        #region Get Specialization
        var specs = new UpdateSpecializationSpecification(specializationId);
        var specialization = await _unitOfWork.Repo<Specialization>().GetEntityWithSpecAsync(specs);

        if (specialization is null)
            return NotFound<UpdateSpecializationResponse>($"No specialization with id {specializationId}");
        #endregion

        #region Update Specialization
        if (!string.IsNullOrWhiteSpace(updatedSpecialization.Name))
            specialization.Name = updatedSpecialization.Name;

        if (!string.IsNullOrWhiteSpace(updatedSpecialization.Description))
            specialization.Description = updatedSpecialization.Description;
        #endregion

        #region Update Image
        if (updatedSpecialization.Image != null)
        {
            if (!string.IsNullOrEmpty(specialization.Image))
            {
                await _fileHandler.RemoveImg(specialization.Image);
            }

            var result = await _fileHandler.HandleFile(updatedSpecialization.Image, new FileValidation
            {
                AllowedExtenstions = new[] { ".jpg", ".jpeg", ".png" },
                MaxSize = 5 * 1024 * 1024
            });

            if (!result.Success)
            {
                return BadRequest<UpdateSpecializationResponse>(errors: [result.Error]);
            }

            specialization.Image = result.RelativeFilePath;
            await _fileHandler.SaveFile(updatedSpecialization.Image, result.FullFilePath);
        }
        #endregion

        if (await _unitOfWork.SaveChangesAsync())
        {
            var response = new UpdateSpecializationResponse
            {
                Id = specialization.Id,
                Name = specialization.Name,
                Description = specialization.Description,
                Image = DoctorMappingExtensions.GetImgUrl(specialization.Image, _httpContextAccessor)
            };

            return Success(response, "Specialization updated successfully");
        }

        return BadRequest<UpdateSpecializationResponse>("No changes made");
    }


}