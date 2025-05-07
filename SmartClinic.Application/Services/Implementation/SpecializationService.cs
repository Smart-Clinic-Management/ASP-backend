using SmartClinic.Application.Features.Doctors.Query.GetDoctor;
using SmartClinic.Application.Features.Specializations.Query.GetSpecialization;
using SmartClinic.Application.Features.Specializations.Query.GetSpecializations;
using SmartClinic.Application.Services.Implementation.Specifications.DoctorSpecifications.GetDoctorByIdSpecifications;
using SmartClinic.Application.Services.Implementation.Specifications.SpecializationSpecifications.GetSpecializationByIdSpecifications;
using SmartClinic.Application.Services.Implementation.Specifications.SpecializationSpecifications.GetSpecializationsSpecifications;
using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

namespace SmartClinic.Application.Services.Implementation;

public class SpecializationService : ResponseHandler, ISpecializationService
{
    //private readonly ISpecializationRepository _specialRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileHandlerService _fileHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly IPagedCreator<Doctor> _pagedCreator;
    public SpecializationService(
    //ISpecializationRepository specialRepo,
    IFileHandlerService fileHandler,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork,
     IPagedCreator<Doctor> pagedCreator,
    UserManager<AppUser> userManager)
    {
        //_specialRepo = specialRepo;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _fileHandler = fileHandler;
        _httpContextAccessor = httpContextAccessor;
        this._pagedCreator = pagedCreator;


    }

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
        {
            return NotFound<GetSpecializationByIdResponse?>($"No Specialization with id : {SpecializationId}");
           
        }
        return Success(Specialization);
    }
    //        public async Task<Response<CreateSpecializationResponse>> CreateSpecializationAsync(CreateSpecializationRequest request)
    //        {
    //            var fileValidationOptions = new FileValidation
    //            {
    //                AllowedExtenstions = new[] { ".jpg", ".png", ".jpeg" },
    //                MaxSize = 5 * 1024 * 1024 
    //            };

    //            var fileValidationResult = await _fileHandler.HanldeFile(request.Image, fileValidationOptions);

    //            if (!fileValidationResult.Success)
    //            {
    //                var errors = new List<string>();
    //                if (!string.IsNullOrEmpty(fileValidationResult.Error)) errors.Add(fileValidationResult.Error);
    //                return new ResponseHandler().BadRequest<CreateSpecializationResponse>(errors: errors);
    //            }

    //            if (fileValidationResult.Success)
    //            {
    //                await _fileHandler.SaveFile(request.Image, fileValidationResult.FullFilePath);
    //            }

    //            var imageUrl = fileValidationResult.RelativeFilePath;

    //            var specialization = request.ToSpecialization(imageUrl, "admin");

    //            _specialRepo.AddAsync(specialization);

    //            var success = await _unitOfWork.SaveChangesAsync();
    //            if (!success)
    //                throw new Exception("Error creating specialization.");

    //            var response = new CreateSpecializationResponse
    //            {
    //                Id = specialization.Id,
    //                Name = specialization.Name,
    //                Description = specialization.Description,
    //                Image = specialization.Image, 
    //            };

    //            return new ResponseHandler().Success(response);

    //        }


    //        public async Task<Response<CreateSpecializationResponse>> GetSpecializationByIdAsync(int specializationId)
    //        {
    //            var specialization = await _specialRepo.GetByIdWithIncludesAsync(specializationId);

    //            if (specialization is null || !specialization.IsActive)
    //                return new ResponseHandler().NotFound<CreateSpecializationResponse>($"No Specialization with id {specializationId}");

    //            var response = new CreateSpecializationResponse
    //            {
    //                Id = specialization.Id,
    //                Name = specialization.Name,
    //                Description = specialization.Description,
    //                Image = _fileHandler.GetFileURL(specialization.Image),
    //                Doctors = specialization.Doctors
    //                    .Where(d => d.IsActive)
    //                    .Select(d => new DoctorDto
    //                    {
    //                        Id = d.Id,
    //                        Name = d.User != null
    //                                ? $"{d.User.FirstName} {d.User.LastName}".Trim()
    //                                    : "No User Linked",
    //                        Description = d.Description,
    //                        IsActive = d.IsActive,
    //                        Image = d.User != null ? _fileHandler.GetFileURL(d.User.ProfileImage)
    //                            : null,
    //                    }).ToList()
    //            };

    //            return new ResponseHandler().Success(response);

    //        }

    //        public async Task<Response<UpdateSpecializationResponse>> UpdateSpecializationAsync(int specializationId, UpdateSpecializationRequest request)
    //        {
    //            var specialization = await _specialRepo.GetByIdWithIncludesAsync(specializationId)
    //                      ?? throw new Exception("Specialization not found");

    //            if (request.Image != null)
    //            {
    //                var validationOptions = new FileValidation
    //                {
    //                    MaxSize = 2 * 1024 * 1024,
    //                    AllowedExtenstions = [".jpg", ".jpeg", ".png"]
    //                };

    //                var fileResult = await _fileHandler.HanldeFile(request.Image, validationOptions);

    //                if (!fileResult.Success)
    //                {
    //                    var errors = new List<string> { fileResult.Error };
    //                    return new ResponseHandler().BadRequest<UpdateSpecializationResponse>(errors);
    //                }

    //                specialization.Image = fileResult.RelativeFilePath;
    //                await _fileHandler.SaveFile(request.Image, fileResult.FullFilePath);
    //            }

    //            specialization.UpdateSpecializationFromRequest(request); 

    //            _specialRepo.Update(specialization);

    //            var success = await _unitOfWork.SaveChangesAsync();
    //            if (!success)
    //                throw new Exception("Error updating specialization.");

    //            // Get full URL
    //            var imageUrl = SpecializationMapperExtentions.GetImgUrl(specialization.Image, _httpContextAccessor);

    //            var response = new UpdateSpecializationResponse
    //            {
    //                Id = specialization.Id,
    //                Name = specialization.Name,
    //                Description = specialization.Description,
    //                IsActive = specialization.IsActive,
    //                Image = imageUrl
    //            };

    //            return new ResponseHandler().Success(response, message: "Specialization updated successfully");
    //        }

    //        public async Task<Response<List<CreateSpecializationResponse>>> GetAllSpecializationsAsync()
    //        {
    //            var specializations = await _specialRepo.ListNoTrackingAsync();

    //            var response = specializations
    //                .Where(s => s.IsActive) 
    //                .Select(s => new CreateSpecializationResponse
    //                {
    //                    Id = s.Id,
    //                    Name = s.Name,
    //                    Description = s.Description,
    //                    Image = _fileHandler.GetFileURL(s.Image),
    //                    Doctors = s.Doctors
    //                        .Where(d => d.IsActive) 
    //                        .Select(d => new DoctorDto
    //                        {
    //                            Id = d.Id,
    //                            Name = d.User != null
    //                                ? $"{d.User.FirstName} {d.User.LastName}".Trim()
    //    :                           "No User Linked",
    //                            Description = d.Description,
    //                            IsActive = d.IsActive
    //                        })
    //                        .ToList() 
    //                })
    //                .ToList();

    //            return new ResponseHandler().Success(response);

    //        }


    //        public async Task<Response<string>> DeleteSpecializationAsync(int specializationId)
    //        {
    //            var specialization = await _specialRepo.GetByIdAsync(specializationId);

    //            if (specialization is null)
    //                return new ResponseHandler().NotFound<string>($"No specialization with id {specializationId}");

    //            specialization.IsActive = false; // Soft delete by deactivating
    //            _specialRepo.Update(specialization);

    //            var success = await _unitOfWork.SaveChangesAsync();

    //            if (!success)
    //                throw new Exception("Error deleting specialization.");

    //            return new ResponseHandler().Success<string>("Specialization deleted successfully (soft delete)");
    //        }


    //        private CreateSpecializationResponse MapSpecializationToResponse(Specialization specialization)
    //        {
    //            return new CreateSpecializationResponse
    //            {
    //                Id = specialization.Id,
    //                Name = specialization.Name,
    //                Description = specialization.Description,
    //                Image = specialization.Image
    //        };
    //}
    //}
}