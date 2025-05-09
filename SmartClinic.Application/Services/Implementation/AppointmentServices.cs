using SmartClinic.Application.Features.Appointments.Mapper;
using SmartClinic.Application.Features.Appointments.Query.AllAppointments;
using SmartClinic.Application.Features.Appointments.Query.DoctorAppointments;
using SmartClinic.Application.Features.Appointments.Query.PatientAppointments;
using SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.AllAppointmentsSpecifications;
using SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.CreateAppointmentSpecifications;
using SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.GetDoctorAppointmentsSpecifications;
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

    public async Task<Response<Pagination<AllAppointmentsResponseDto>>> ListAllAppointmentsAsync(AllAppointmentsParams appointmentsParams)
    {
        #region Validation
        var validator = new AllAppointmentValidator();

        var validationResult = await validator.ValidateAsync(appointmentsParams);

        if (!validationResult.IsValid)
            return BadRequest<Pagination<AllAppointmentsResponseDto>>(errors: [.. validationResult.Errors.Select(x => x.ErrorMessage)]);

        #endregion

        var specs = new AllAppointmentsSpecification(appointmentsParams);

        var appointments = await pagedCreator.CreatePagedResult(_unitOfWork.Repo<Appointment>(),
            specs, appointmentsParams.PageIndex, appointmentsParams.PageSize);

        if (appointments.Total == 0)
            return NotFound<Pagination<AllAppointmentsResponseDto>>("There is no appointment found");

        UpdateAppointmentsStatus(appointments);

        await _unitOfWork.SaveChangesAsync();

        var result = appointments.ToAllAppointmentsPaginatedDto();

        return Success(result);
    }

    public async Task<Response<Pagination<DoctorWithAppointmentsResponseDto>>> ListDoctorAppointmentsAsync(int doctorId, GetDoctorAppointmentsParams appointmentsParams)
    {
        #region Validation
        var validator = new GetDoctorAppointmentValidator();

        var validationResult = await validator.ValidateAsync(appointmentsParams);

        if (!validationResult.IsValid)
            return BadRequest<Pagination<DoctorWithAppointmentsResponseDto>>(errors: [.. validationResult.Errors.Select(x => x.ErrorMessage)]);

        #endregion

        var specs = new GetDoctorAppointmentsSpecification(doctorId, appointmentsParams);

        var appointments = await pagedCreator.CreatePagedResult(_unitOfWork.Repo<Appointment>(),
            specs, appointmentsParams.PageIndex, appointmentsParams.PageSize);

        if (appointments.Total == 0)
            return NotFound<Pagination<DoctorWithAppointmentsResponseDto>>("There is no appointment found");

        UpdateAppointmentsStatus(appointments);

        await _unitOfWork.SaveChangesAsync();

        var result = appointments.ToDoctorAppointmentsPaginatedDto();

        return Success(result);
    }

    public async Task<Response<Pagination<PatientAppointmentsWithDoctorDetailsDto>>> ListPatientAppointmentsAsync(int patientId, GetPatientAppointmentsParams appointmentsParams)
    {
        #region Validation
        var validator = new GetPatientAppointmentValidator();

        var validationResult = await validator.ValidateAsync(appointmentsParams);

        if (!validationResult.IsValid)
            return BadRequest<Pagination<PatientAppointmentsWithDoctorDetailsDto>>(errors: [.. validationResult.Errors.Select(x => x.ErrorMessage)]);

        #endregion

        var specs = new GetPatientAppointmentsSpecification(patientId, appointmentsParams);

        var appointments = await pagedCreator.CreatePagedResult(_unitOfWork.Repo<Appointment>(),
            specs, appointmentsParams.PageIndex, appointmentsParams.PageSize);

        if (appointments.Total == 0)
            return NotFound<Pagination<PatientAppointmentsWithDoctorDetailsDto>>("There is no appointment found");

        UpdateAppointmentsStatus(appointments);

        await _unitOfWork.SaveChangesAsync();

        var result = appointments.ToPatientAppointmentsPaginatedDto();

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
