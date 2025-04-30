using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartClinic.Application.Features.Specializations.Command.DTOs.CreateSpecialization;
using SmartClinic.Application.Features.Specializations.Command.DTOs.UpdateSpecialization;
using SmartClinic.Application.Features.Specializations.Mapper;

namespace SmartClinic.Application.Services.Implementation
{
    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializaionRepository _specialRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public SpecializationService(
        ISpecializaionRepository specialRepo,
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager)
        {
            _specialRepo = specialRepo;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<Response<CreateSpecializationResponse>> CreateSpecializationAsync(CreateSpecializationRequest request)
        {
            // جهزي اسم الصورة
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";

            // مسار حفظ الصورة على السيرفر
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Specializations", imageName);

            // تأكدي الفولدر موجود
            var directory = Path.GetDirectoryName(imagePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // احفظي الصورة
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            // حضري URL الصورة (بعد الحفظ)
            var imageUrl = $"https://localhost:7047/Images/Specializations/{imageName}"; // تأكدي البورت صح حسب بروجيكتك

            // استخدمي الـ mapper مع رابط الصورة
            var specialization = request.ToSpecialization(imageUrl, "admin"); // userId حطيته مؤقتًا "admin" لو عايزة تضيفي في المستقبل

            _specialRepo.AddAsync(specialization);

            var success = await _unitOfWork.SaveChangesAsync();
            if (!success)
                throw new Exception("Error creating specialization.");

            var response = new CreateSpecializationResponse
            {
                Id = specialization.Id,
                Name = specialization.Name,
                Description = specialization.Description,
                Image = specialization.Image
            };

            return new Response<CreateSpecializationResponse>(response);
        }


        public async Task<Response<CreateSpecializationResponse>> GetSpecializationByIdAsync(int specializationId)
        {
            var specialization = await _specialRepo.GetByIdWithIncludesAsync(specializationId);

            if (specialization is null || !specialization.IsActive)
                return new ResponseHandler().NotFound<CreateSpecializationResponse>($"No Specialization with id {specializationId}");

            var response = new CreateSpecializationResponse
            {
                Id = specialization.Id,
                Name = specialization.Name,
                Description = specialization.Description,
                Image = specialization.Image,
                Doctors = specialization.Doctors
                    .Where(d => d.IsActive)
                    .Select(d => new DoctorDto
                    {
                        Id = d.Id,
                        Name = d.User != null ? d.User.UserName : "No User Linked",
                        Description = d.Description,
                        IsActive = d.IsActive
                    }).ToList()
            };

            return new Response<CreateSpecializationResponse>(response);
        }


        public async Task<Response<CreateSpecializationResponse>> UpdateSpecializationAsync(int specializationId, UpdateSpecializationRequest request)
        {
            var specialization = await _specialRepo.GetByIdWithIncludesAsync(specializationId)
                      ?? throw new Exception("Specialization not found");
            specialization.UpdateSpecializationFromRequest(request);
            _specialRepo.Update(specialization);
            var success = await _unitOfWork.SaveChangesAsync();
            if (!success)
                throw new Exception("Error updating specialization.");
            var response = MapSpecializationToResponse(specialization);
            return new Response<CreateSpecializationResponse>(response);

        }

        public async Task<Response<List<CreateSpecializationResponse>>> GetAllSpecializationsAsync()
        {
            var specializations = await _specialRepo.ListNoTrackingAsync();

            var response = specializations
                .Where(s => s.IsActive) 
                .Select(s => new CreateSpecializationResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Image = s.Image,
                    Doctors = s.Doctors
                        .Where(d => d.IsActive) 
                        .Select(d => new DoctorDto
                        {
                            Id = d.Id,
                            Name = d.User != null
                                ? $"{d.User.FirstName} {d.User.LastName}".Trim()
    :                           "No User Linked",
                            Description = d.Description,
                            IsActive = d.IsActive
                        })
                        .ToList() 
                })
                .ToList();

            return new Response<List<CreateSpecializationResponse>>(response);
        }


        public async Task<Response<string>> DeleteSpecializationAsync(int specializationId)
        {
            var specialization = await _specialRepo.GetByIdAsync(specializationId);

            if (specialization is null)
                return new ResponseHandler().NotFound<string>($"No specialization with id {specializationId}");

            specialization.IsActive = false; // Soft delete by deactivating
            _specialRepo.Update(specialization);

            var success = await _unitOfWork.SaveChangesAsync();

            if (!success)
                throw new Exception("Error deleting specialization.");

            return new ResponseHandler().Success<string>("Specialization deleted successfully (soft delete)");
        }


        private CreateSpecializationResponse MapSpecializationToResponse(Specialization specialization)
        {
            return new CreateSpecializationResponse
            {
                Id = specialization.Id,
                Name = specialization.Name,
                Description = specialization.Description,
                Image = specialization.Image
        };
}
}
}
