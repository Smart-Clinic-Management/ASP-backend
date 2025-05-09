namespace SmartClinic.Application.Features.Appointments.Command.UpdateAppointmnet;
public class UpdateAppointmentRequestValidator : AbstractValidator<UpdateAppointmentRequest>
{
    public UpdateAppointmentRequestValidator()
    {
        RuleFor(x => x.Status)
            .Must(x => Enum.IsDefined(x))
            .WithMessage("Invalid appointment status")
            .NotEmpty();

        RuleFor(x => x.AppointmentId)
            .NotEmpty();
    }
}
