using SmartClinic.Application.Features.DoctorsSchedules.Command.DeleteDoctorSchedule;
using SmartClinic.Application.Services.Implementation.Specifications.DoctorSchedulesSpecifications.DeleteDoctorSchedulesSpecifications;

namespace SmartClinic.Application.Services.Implementation;

public class DoctorScheduleServices(IUnitOfWork unitOfWork)
    : ResponseHandler, IDoctorScheduleService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    //public async Task<Response<IEnumerable<GetDoctorSchedule>>> GetSchedulesForDoctorAsync(int doctorId)
    //{
    //    var schedules = await _doctorScheduleRepo.GetByDoctorIdAsync(doctorId);

    //    if (schedules == null || !schedules.Any())
    //        return NotFound<IEnumerable<GetDoctorSchedule>>();

    //    var scheduleDtos = schedules.Select(schedule => schedule.ToGetDoctorScheduleDto()).ToList();

    //    return Success<IEnumerable<GetDoctorSchedule>>(scheduleDtos);
    //}

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

    //public async Task<Response<GetDoctorSchedule>> CreateAsync(CreateDoctorScheduleRequest request)
    //{
    //    var schedule = request.ToEntity();

    //    await _doctorScheduleRepo.AddAsync(schedule);
    //    await _unitOfWork.SaveChangesAsync();

    //    return Created(schedule.ToGetDoctorScheduleDto());

    //}



    //public async Task<Response<GetDoctorSchedule>> UpdateAsync(UpdateDoctorScheduleRequest request)
    //{
    //    var schedule = await _doctorScheduleRepo.GetByIdAsync(request.Id);

    //    if (schedule is null)
    //        return BadRequest<GetDoctorSchedule>([$"Schedule with ID {request.Id} not found."]);

    //    schedule.UpdateFromRequest(request);

    //    await _unitOfWork.SaveChangesAsync();

    //    return Success(schedule.ToGetDoctorScheduleDto());

    //}


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
