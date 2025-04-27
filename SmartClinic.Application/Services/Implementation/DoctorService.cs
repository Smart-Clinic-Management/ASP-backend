using Microsoft.AspNetCore.Http;
using SmartClinic.Application.Services.Implementation.FileHandlerService.Command;

namespace SmartClinic.Application.Services.Implementation
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileHandlerService _fileHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISpecializationService _specializationService;

        public DoctorService(
            IDoctorRepository doctorRepo,
            IUnitOfWork unitOfWork,
            UserManager<AppUser> userManager,
            IFileHandlerService fileHandler,
            IHttpContextAccessor httpContextAccessor,
            ISpecializationService specializationService)
        {
            _doctorRepo = doctorRepo;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _fileHandler = fileHandler;
            _httpContextAccessor = httpContextAccessor;
            _specializationService = specializationService;
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

        public async Task<Response<List<GetAllDoctorsResponse>>> GetAllDoctorsAsync(int pageSize = 20, int pageIndex = 1)
        {
            var doctors = await _doctorRepo.ListAsync(pageSize, pageIndex);

            var response = doctors.Select(doctor =>
            {
                var imageUrl = DoctorMappingExtensions.GetImgUrl(doctor.User.ProfileImage, _httpContextAccessor);

                var doctorResponse = doctor.ToGetAllDoctorsResponse();
                return doctorResponse with { image = imageUrl };
            }).ToList();

            return new ResponseHandler().Success(response);
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

            var user = await _userManager.FindByIdAsync(doctor.UserId);
            if (user != null)
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);
            }

            await _unitOfWork.SaveChangesAsync();

            return new ResponseHandler().Success(new SoftDeleteDoctorResponse("Doctor and associated user successfully soft deleted."));
        }

        public async Task<Response<CreateDoctorResponse>> CreateDoctor(CreateDoctorRequest newDoctorUser)
        {
            if (newDoctorUser.Image == null)
            {
                return new ResponseHandler().BadRequest<CreateDoctorResponse>(errors: ["No image uploaded"]);
            }

            var validationOptions = new FileValidation
            {
                MaxSize = 2 * 1024 * 1024,
                AllowedExtenstions = [".jpg", ".jpeg", ".png"]
            };

            var fileResult = await _fileHandler.HanldeFile(newDoctorUser.Image, validationOptions);

            if (!fileResult.Success)
            {
                var errors = new List<string>();
                if (!string.IsNullOrEmpty(fileResult.Error)) errors.Add(fileResult.Error);
                return new ResponseHandler().BadRequest<CreateDoctorResponse>(errors: errors);
            }

            var user = new AppUser
            {
                UserName = newDoctorUser.Email,
                Email = newDoctorUser.Email,
                FirstName = newDoctorUser.Fname,
                LastName = newDoctorUser.Lname,
                Address = newDoctorUser.Address,
                BirthDate = newDoctorUser.BirthDate,
                ProfileImage = fileResult.RelativeFilePath
            };

            var userCreationResult = await _userManager.CreateAsync(user, "DefaultPassword123");

            if (!userCreationResult.Succeeded)
            {
                var errors = userCreationResult.Errors.Select(e => e.Description).ToList();
                return new ResponseHandler().BadRequest<CreateDoctorResponse>(errors);
            }

            await _userManager.AddToRoleAsync(user, "Doctor");

            var doctor = new Doctor
            {
                UserId = user.Id,
                Description = newDoctorUser.Description,
                WaitingTime = newDoctorUser.WaitingTime,
                IsActive = true
            };

            await doctor.AddSpecializationsToDoctorAsync(_specializationService, newDoctorUser.Specialization);

            await _doctorRepo.AddAsync(doctor);

            if (fileResult.Success)
            {
                await _fileHandler.SaveFile(newDoctorUser.Image, fileResult.FullFilePath);
            }

            await _unitOfWork.SaveChangesAsync();

            var response = new CreateDoctorResponse(
                Fname: user.FirstName,
                Lname: user.LastName,
                Email: user.Email,
                Image: fileResult.Success ? DoctorMappingExtensions.GetImgUrl(fileResult.RelativeFilePath, _httpContextAccessor) : null,
                Specialization: doctor.Specializations.Select(s => s.Id).ToList(),
                BirthDate: user.BirthDate,
                Address: user.Address,
                WaitingTime: doctor.WaitingTime,
                Description: doctor.Description
            );

            return new ResponseHandler().Success(response, message: "Doctor Created Successfully");
        }

        public async Task<Response<UpdateDoctorResponse>> UpdateDoctorAsync(int doctorId, UpdateDoctorRequest request)
        {
            var doctor = await _doctorRepo.GetByIdWithIncludesAsync(doctorId);
            if (doctor == null || !doctor.IsActive)
            {
                return new ResponseHandler().NotFound<UpdateDoctorResponse>($"No Doctor found with id {doctorId}");
            }

            var user = doctor.User;
            user.FirstName = request.Fname ?? user.FirstName;
            user.LastName = request.Lname ?? user.LastName;
            user.Email = request.Email ?? user.Email;
            user.Address = request.Address ?? user.Address;

            if (request.Image != null)
            {
                var validationOptions = new FileValidation
                {
                    MaxSize = 2 * 1024 * 1024,
                    AllowedExtenstions = [".jpg", ".jpeg", ".png"]
                };

                var fileResult = await _fileHandler.HanldeFile(request.Image, validationOptions);

                if (!fileResult.Success)
                {
                    var errors = new List<string> { fileResult.Error };
                    return new ResponseHandler().BadRequest<UpdateDoctorResponse>(errors);
                }

                user.ProfileImage = fileResult.RelativeFilePath;
                await _fileHandler.SaveFile(request.Image, fileResult.FullFilePath);
            }

            await doctor.AddSpecializationsToDoctorAsync(_specializationService, request.Specialization);

            await _unitOfWork.SaveChangesAsync();

            var imageUrl = DoctorMappingExtensions.GetImgUrl(user.ProfileImage, _httpContextAccessor);

            var response = doctor.ToUpdateDoctorResponse(_httpContextAccessor) with { Image = imageUrl };

            return new ResponseHandler().Success(response, message: "Doctor updated successfully");
        }
    }
}
