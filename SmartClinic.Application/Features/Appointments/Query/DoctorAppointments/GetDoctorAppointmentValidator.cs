namespace SmartClinic.Application.Features.Appointments.Query.DoctorAppointments;
public class GetDoctorAppointmentValidator : AbstractValidator<GetDoctorAppointmentsParams>
{
    public GetDoctorAppointmentValidator()
    {
        RuleFor(x => x.PageIndex)
         .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
           .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(20);

        RuleFor(x => x.OrderBy)
            .Must(ValidProperty).WithMessage("Invalid Property Name");
    }

    private bool ValidProperty(string? orderBy)
    {
        if (orderBy is null) return true;

        return PagingValidator.IsValidProperty(orderBy, typeof(PatientAppointmentsWithDoctorDetailsDto));
    }
}
