using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Doctors.Command.DTOs.DeleteDoctor;
using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctor;
using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctors;

namespace SmartClinic.Application.Services.Implementation
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public DoctorService(
            IDoctorRepository doctorRepo,
            IUnitOfWork unitOfWork,
            UserManager<AppUser> userManager)
        {
            _doctorRepo = doctorRepo;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Response<GetDoctorByIdResponse>> GetDoctorByIdAsync(int doctorId)
        {
            var doctor = await _doctorRepo.GetByIdWithIncludesAsync(doctorId);
            if (doctor == null)
                return new ResponseHandler().NotFound<GetDoctorByIdResponse>($"No doctor found with ID {doctorId}");

            var response = doctor.ToGetDoctorByIdResponse();
            return new ResponseHandler().Success(response);
        }

        public async Task<Response<List<GetAllDoctorsResponse>>> GetAllDoctorsAsync(int pageSize = 20, int pageIndex = 1)
        {
            var doctors = await _doctorRepo.ListAsync(pageSize, pageIndex);

            var response = doctors.Select(doctor => doctor.ToGetAllDoctorsResponse()).ToList();
            return new ResponseHandler().Success(response);
        }

        public async Task<Response<UpdateDoctorResponse>> UpdateDoctorAsync(int doctorId, UpdateDoctorRequest request)
        {
            var doctor = await _doctorRepo.GetByIdWithIncludesAsync(doctorId);
            if (doctor is null)
                return new ResponseHandler().NotFound<UpdateDoctorResponse>($"No Doctor found with id {doctorId}");

            doctor.UpdateDoctorWithRequest(request, doctor.UserId);

            // var specializations = await _specializationRepo.GetByIdsAsync(request.Specializations);
            // doctor.Specializations.Clear();
            // doctor.Specializations.AddRange(specializations);

            await _unitOfWork.SaveChangesAsync();

            var response = doctor.ToUpdateDoctorResponse();
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




    }
}
