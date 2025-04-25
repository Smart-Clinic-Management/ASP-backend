using SmartClinic.Application.Bases;

namespace SmartClinic.Application.Services.Implementation;

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

    public async Task<Response<Doctor?>> GetDoctorByIdAsync(int doctorId)
    {
        var doctor = await _doctorRepo.GetByIdWithIncludesAsync(doctorId);
        if (doctor is null)
            return new ResponseHandler().NotFound<Doctor?>($"No Doctor with id {doctorId}");

        return new ResponseHandler().Success<Doctor?>(doctor);

    }

    public async Task<Response<CreateDoctorResponse>> UpdateDoctorAsync(int doctorId, UpdateDoctorRequest request)
    {
        var doctor = await _doctorRepo.GetByIdWithIncludesAsync(doctorId)
                  ?? throw new Exception("Doctor not found");

        doctor.UpdateDoctorFromRequest(request);

        _doctorRepo.Update(doctor);

        var success = await _unitOfWork.SaveChangesAsync();
        if (!success)
            throw new Exception("Error updating doctor.");

        var response = MapDoctorToResponse(doctor);

        return null;
    }




    private CreateDoctorResponse MapDoctorToResponse(Doctor doctor)
    {
        return new CreateDoctorResponse
        {
            Id = doctor.Id,
            SpecializationId = doctor.Specializations.FirstOrDefault()?.Id ?? 0,
            Description = doctor.Description,
            WaitingTime = doctor.WaitingTime,
            Address = doctor.User.Address,
            BirthDate = doctor.User.BirthDate,
            UserId = doctor.UserId,
            FirstName = doctor.User.FirstName,
            LastName = doctor.User.LastName ?? "",
            UserEmail = doctor.User.Email ?? "",
            UserPhoneNumber = doctor.User.PhoneNumber ?? ""
        };
    }


}
