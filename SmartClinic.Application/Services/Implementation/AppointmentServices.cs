﻿namespace SmartClinic.Application.Services.Implementation;

public class AppointmentService(IUnitOfWork unitOfWork, IPagedCreator<Appointment> pagedCreator, IEmailSender emailSender)
        : ResponseHandler,
    IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Response<string>> CreateAppointmentAsync(CreateAppointmentRequest appointmentDto, MailData receiverData)
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

        var appointment = appointmentDto.ToEntity(receiverData.Id, scheduleTimeSlot);

        await _unitOfWork.Repo<Appointment>().AddAsync(appointment);

        await _unitOfWork.SaveChangesAsync();

        #region Send Mail
        var emailMessage = receiverData.GeneratePatientAppointmentMessage(appointment, doctor);

        var mailSent = await emailSender.SendEmailAsync(emailMessage);

        return Created("Created and mail Sent to patient");

        #endregion
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

    public async Task<Response<string>> UpdateDoctorAppointmentAsync(MailData doctorData, UpdateAppointmentRequest updateAppointment)
    {
        #region Validation

        var validator = new UpdateAppointmentRequestValidator();

        var validationResult = await validator.ValidateAsync(updateAppointment);

        if (!validationResult.IsValid)
            return BadRequest<string>(errors: [.. validationResult.Errors.Select(x => x.ErrorMessage)]);

        #endregion

        var specs = new UpdateDoctorAppointmentSpecification(doctorData.Id, updateAppointment);

        var appointment = await _unitOfWork.Repo<Appointment>().GetEntityWithSpecAsync(specs);

        if (appointment is null)
            BadRequest<string>($"Invalid appointment id");


        #region Check if the appointment can have the new status

        switch (updateAppointment.Status)
        {
            case AppointmentStatus.Pending:
                if (CanBePendingAppointment(appointment!))
                    appointment!.Status = updateAppointment.Status;
                else
                    return BadRequest<string>("you can't change passed appointment to pending status");
                break;
            case AppointmentStatus.Canceled:
                appointment!.Status = updateAppointment.Status;
                #region Send Mail

                var patientMessage = appointment.GeneratePatientCancelInformMessage(doctorData.Name);
                await emailSender.SendEmailAsync(patientMessage);
                #endregion
                break;
            case AppointmentStatus.Completed:
                if (CanBeCompletedAppointment(appointment!))
                    appointment!.Status = updateAppointment.Status;
                else
                    return BadRequest<string>("you can't change unpasted appointment to completed status");
                break;
        }

        #endregion

        await _unitOfWork.SaveChangesAsync();

        return Success("", "Updated Successfully");
    }

    public async Task<Response<string>> CancelPatientAppointmentAsync(MailData patientData, int appointmentId)
    {

        var specs = new CancelAppointmentSpecification(patientData.Id, appointmentId);

        var appointment = await _unitOfWork.Repo<Appointment>().GetEntityWithSpecAsync(specs);

        if (appointment is null)
            BadRequest<string>($"Invalid appointment id");



        if (appointment!.Status != AppointmentStatus.Pending)
            return BadRequest<string>("you can't cancel completed or canceled appointment");

        appointment!.Status = AppointmentStatus.Canceled;

        await _unitOfWork.SaveChangesAsync();

        #region Send Mails

        var patientMessage = patientData.GeneratePatientCancelAppointmentMessage(appointment);
        await emailSender.SendEmailAsync(patientMessage);

        var doctorMessage = patientData.GenerateDoctorCancelAppointmentMessage(appointment);
        await emailSender.SendEmailAsync(doctorMessage);

        #endregion

        return Success("", "Canceled Successfully and mails sent to the patient and the doctor");
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

    private static bool CanBePendingAppointment(Appointment appointment)
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        var currentTime = TimeOnly.FromDateTime(DateTime.Now);

        return appointment!.AppointmentDate > currentDate
                            || (appointment.Duration.EndTime > currentTime && appointment.AppointmentDate == currentDate);
    }

    private static bool CanBeCompletedAppointment(Appointment appointment)
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        var currentTime = TimeOnly.FromDateTime(DateTime.Now);

        return appointment!.AppointmentDate < currentDate
                    || (appointment.Duration.EndTime < currentTime && appointment.AppointmentDate == currentDate);
    }


}
