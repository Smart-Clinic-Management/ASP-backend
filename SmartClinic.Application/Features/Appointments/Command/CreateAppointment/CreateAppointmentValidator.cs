using FluentValidation;

namespace SmartClinic.Application.Features.Appointments.Command.CreateAppointment;
public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.AppointmentDate)
            .NotEmpty()
            .Must(ValidDate).WithMessage("Not valid date , date must be at least today");

        RuleFor(x => x.StartTime)
         .NotEmpty();


        RuleFor(x => x.TimeSlot)
        .GreaterThanOrEqualTo(0)
        .LessThanOrEqualTo(60)
        .NotEmpty();

        RuleFor(x => x.DoctorId)
         .MustAsync(DoctorExists).WithMessage("Invalid Doctor Id")
         .NotEmpty();

        RuleFor(x => x.SpecializationId)
         .NotEmpty();

        RuleFor(x => x)
            .MustAsync(IsValidDoctorSpecialization).WithMessage("Invalid Doctor Specialization Id")
            .MustAsync(ValidAppointment).WithMessage("Appointment already reserved or invalid inserted data");


        this._unitOfWork = unitOfWork;
    }


    private async Task<bool> IsValidDoctorSpecialization(CreateAppointmentDto dto, CancellationToken token)
        => await _unitOfWork.Repository<IDoctorRepository>().
                     IsValidDoctorSpecialization(dto.SpecializationId, dto.DoctorId);

    private async Task<bool> ValidAppointment(CreateAppointmentDto dto, CancellationToken token)
    {
        var doctor = await _unitOfWork.Repository<IDoctorRepository>()
            .GetDoctorWithSpecificScheduleAsync(dto.DoctorId, dto.AppointmentDate,
            dto.StartTime, dto.TimeSlot);
        if (doctor is null || doctor.DoctorSchedules.Count == 0 || doctor.Appointments.Count > 0) return false;

        return true;
    }

    private async Task<bool> DoctorExists(int DoctorId, CancellationToken token)
         => await _unitOfWork.Repository<IDoctorRepository>().ExistsAsync(DoctorId);



    private bool ValidDate(DateOnly appointmentDate)
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        return appointmentDate >= currentDate;
    }


}
