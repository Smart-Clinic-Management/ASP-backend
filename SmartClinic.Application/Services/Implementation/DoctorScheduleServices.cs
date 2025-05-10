using SmartClinic.Application.Features.DoctorsSchedules.Command.CreateDoctorSchedule;
using SmartClinic.Application.Features.DoctorsSchedules.Mapper;
using SmartClinic.Application.Features.DoctorsSchedules.Query.DTOs.GetDoctorSchedule;
using SmartClinic.Application.Services.Implementation.Specifications.DoctorSchedulesSpecifications.CreateDoctorSchedulesSpecifications;

namespace SmartClinic.Application.Services.Implementation;

public class DoctorScheduleServices(IUnitOfWork unitOfWork)
    : ResponseHandler, IDoctorScheduleService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Response<string>> DeleteScheduleAsync(DeleteDoctorScheduleRequest deleteDoctorSchedule)
    {
        var specs = new DeleteDoctorSchedulesSpecification(deleteDoctorSchedule);

        var doctor = await _unitOfWork.Repo<Doctor>().GetEntityWithSpecAsync(specs);

        if (doctor is null || !doctor.DoctorSchedules.Any())
            return NotFound<string>();

        CancelUpcommingAppointments(doctor.Appointments);

        _unitOfWork.Repo<DoctorSchedule>().Delete(doctor.DoctorSchedules.FirstOrDefault()!);

        if (await _unitOfWork.SaveChangesAsync())
            return Success<string>(null, "Deleted Successfully and all upcoming schedule appointment are canceled");

        return BadRequest<string>("Something went wrong while deleting schedule");

    }

    public async Task<Response<GetDoctorSchedule>> CreateAsync(CreateDoctorScheduleRequest request)
    {
        #region Validation

        var validator = new CreateDoctorScheduleRequestValidator(_unitOfWork);

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest<GetDoctorSchedule>(errors: [.. validationResult.Errors.Select(x => x.ErrorMessage)]);

        var specs = new CreateDoctorSchedulesSpecification(request);

        var scheduleExists = await _unitOfWork.Repo<DoctorSchedule>().ExistsWithSpecAsync(specs);

        if (scheduleExists)
            return BadRequest<GetDoctorSchedule>("This schedule is already exists please choose another day");

        #endregion

        var newSchedule = request.ToEntity();

        await _unitOfWork.Repo<DoctorSchedule>().AddAsync(newSchedule);

        if (await _unitOfWork.SaveChangesAsync())
            return Created(newSchedule.ToGetDoctorScheduleDto());

        return BadRequest<GetDoctorSchedule>("Something went wrong while creating the schedule");

    }


    private static void CancelUpcommingAppointments(IEnumerable<Appointment> appointments)
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        var currentTime = TimeOnly.FromDateTime(DateTime.Now);

        foreach (var appointment in appointments)
        {
            if (appointment.Duration.EndTime >= currentTime && appointment.AppointmentDate == currentDate)
                appointment.Status = AppointmentStatus.Canceled;
            else if (appointment.AppointmentDate > currentDate)
                appointment.Status = AppointmentStatus.Canceled;
        }
    }
}
