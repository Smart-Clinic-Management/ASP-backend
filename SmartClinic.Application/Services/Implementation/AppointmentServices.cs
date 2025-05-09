using SmartClinic.Application.Features.Appointments.Mapper;
using SmartClinic.Application.Features.Appointments.Query.PatientAppointments;
using SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.CreateAppointmentSpecifications;
using SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.GetPatientAppointmentsSpecifications;

namespace SmartClinic.Application.Services.Implementation;

public class AppointmentService(IUnitOfWork unitOfWork, IPagedCreator<Appointment> pagedCreator)
        : ResponseHandler,
    IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Response<string>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto, int patientId)
    {
        #region Validation

        var validator = new CreateAppointmentValidator(_unitOfWork);

        var validatinResult = await validator.ValidateAsync(appointmentDto);

        if (!validatinResult.IsValid)
            return BadRequest<string>(errors: [.. validatinResult.Errors.Select(x => x.ErrorMessage)]);

        #endregion

        var specs = new CreateAppointmentSpecification(appointmentDto);

        var doctor = await _unitOfWork.Repo<Doctor>().GetEntityWithSpecAsync(specs);

        // check for the schedule
        if (doctor is null || !doctor.DoctorSchedules.Any())
            return BadRequest<string>("Invalid inserted data");

        // check if there any appointment in the same time
        if (doctor.Appointments.Any())
            return BadRequest<string>("Appointment already reserved");


        var scheduleTimeSlot = doctor.DoctorSchedules.FirstOrDefault()!.SlotDuration;

        var appointment = appointmentDto.ToEntity(patientId, scheduleTimeSlot);

        await _unitOfWork.Repo<Appointment>().AddAsync(appointment);

        if (await _unitOfWork.SaveChangesAsync())
            return Created("Created");

        return BadRequest<string>("Appointment not created");
    }

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

    public async Task<Response<Pagination<PatientAppointmentsWithDoctorDetailsDto>>> ListPatientAppointmentsAsync(int patientId, GetPatientAppointmentParams appointmentParams)
    {
        #region Validation
        var validator = new GetPatientAppointmentValidator();

        var validationResult = await validator.ValidateAsync(appointmentParams);

        if (!validationResult.IsValid)
            return BadRequest<Pagination<PatientAppointmentsWithDoctorDetailsDto>>(errors: [.. validationResult.Errors.Select(x => x.ErrorMessage)]);

        #endregion

        var specs = new GetPatientAppointmentsSpecification(patientId, appointmentParams);

        var appointments = await pagedCreator.CreatePagedResult(_unitOfWork.Repo<Appointment>(), specs, appointmentParams.PageIndex, appointmentParams.PageSize);

        if (appointments.Total == 0)
            return NotFound<Pagination<PatientAppointmentsWithDoctorDetailsDto>>("There is no appointment found");

        UpdateAppointmentsStatus(appointments);

        await _unitOfWork.SaveChangesAsync();

        var result = appointments.ToPaginatedDto();

        return Success(result);
    }

    private static void UpdateAppointmentsStatus(Pagination<Appointment> appointments)
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        var currentTime = TimeOnly.FromDateTime(DateTime.Now);
        foreach (var appointment in appointments.Data.Where(x => x.AppointmentDate <= currentDate))
        {
            if (appointment.Duration.EndTime <= currentTime && appointment.AppointmentDate == currentDate)
                appointment.Status = AppointmentStatus.Canceled;
            else if (appointment.AppointmentDate < currentDate)
                appointment.Status = AppointmentStatus.Canceled;
        }
    }
}
