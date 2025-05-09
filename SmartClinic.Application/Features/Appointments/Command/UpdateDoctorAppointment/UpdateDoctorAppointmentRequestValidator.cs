namespace SmartClinic.Application.Features.Appointments.Command.UpdateDoctorAppointment;
public class UpdateDoctorAppointmentRequestValidator : AbstractValidator<UpdateDoctorAppointmentRequest>
{
    public UpdateDoctorAppointmentRequestValidator()
    {
        RuleFor(x => x.Status)
            .Must(x => Enum.IsDefined<AppointmentStatus>(x))
            .WithMessage("Invalid appointment status")
            .NotEmpty();

        RuleFor(x => x.AppointmentId)
            .NotEmpty();
    }
}
