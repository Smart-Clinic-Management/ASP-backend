using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

namespace SmartClinic.Application.Services.Implementation;

public class AppointmentService
    : ResponseHandler,
    IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    //public async Task<Response<string>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto, int patientId)
    //{
    //    var validator = new CreateAppointmentValidator(_unitOfWork);

    //    var validatinResult = await validator.ValidateAsync(appointmentDto);

    //    if (!validatinResult.IsValid)
    //    {
    //        List<string> errors = [.. validatinResult.Errors.Select(x => x.ErrorMessage)];
    //        return BadRequest<string>(errors);
    //    }

    //    var doctor = await _unitOfWork.Repository<IDoctorRepository>()
    //       .GetDoctorWithSpecificScheduleAsync(appointmentDto.DoctorId, appointmentDto.AppointmentDate,
    //       appointmentDto.StartTime);


    //    if (doctor is null || doctor.DoctorSchedules.Count == 0)
    //        return BadRequest<string>(["Invalid inserted data"]);

    //    if (doctor.Appointments.Count > 0)
    //        return BadRequest<string>(["Appointment already reserved"]);


    //    var scheduleTimeSlot = doctor.DoctorSchedules.FirstOrDefault()!.SlotDuration;

    //    var appointment = appointmentDto.ToEntity(patientId, scheduleTimeSlot);

    //    await _unitOfWork.Repository<IAppointment>().AddAsync(appointment);

    //    if (await _unitOfWork.SaveChangesAsync())
    //        return Created("Created");

    //    return BadRequest<string>(["Appointment not created"]);
    //}

    //public async Task<Response<List<AppointmentResponseDto>>> ListAllAppointmentsAsync(int pageSize = 20, int pageIndex = 1)
    //{
    //    var appointments = await _unitOfWork.Repository<IAppointment>().ListAllAppointmentsAsync(pageSize, pageIndex);
    //    var appointmentDtos = appointments.Select(a => a.ToDto()).ToList();

    //    return new ResponseHandler().Success(appointmentDtos);
    //}

    //public async Task<Response<List<DoctorWithAppointmentsResponseDto>>> ListDoctorAppointmentsAsync(int doctorId, int pageSize = 20, int pageIndex = 1)
    //{
    //    var appointments = await _unitOfWork.Repository<IAppointment>().ListDoctorAppointmentsAsync(doctorId, pageSize, pageIndex);

    //    if (!appointments.Any())
    //        return new ResponseHandler().Success(new List<DoctorWithAppointmentsResponseDto>(), "");

    //    var appointmentDtos = appointments.Select(a => a.ToDoctorDto()).ToList();
    //    return new ResponseHandler().Success(appointmentDtos);
    //}

    //public async Task<Response<List<PatientAppointmentsWithDoctorDetailsDto>>> ListPatientAppointmentsAsync(int patientId, int pageSize = 20, int pageIndex = 1)
    //{
    //    var appointments = await _unitOfWork.Repository<IAppointment>().ListPatientAppointmentsAsync(patientId, pageSize, pageIndex);

    //    if (!appointments.Any())
    //        return new ResponseHandler().Success(new List<PatientAppointmentsWithDoctorDetailsDto>(), "");

    //    var appointmentDtos = appointments.Select(a => a.ToPatientDto()).ToList();
    //    return new ResponseHandler().Success(appointmentDtos);
    //}
}
