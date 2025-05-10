namespace SmartClinic.Application.Features.Appointments.Query.AllAppointments;
public class AllAppointmentValidator : AbstractValidator<AllAppointmentsParams>
{
    public AllAppointmentValidator()
    {
        RuleFor(x => x.PageIndex)
         .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
           .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(20);

        RuleFor(x => x.OrderBy)
            .Must(ValidProperty).WithMessage("Invalid Property Name");

        RuleFor(x => x.DoctorId)
            .GreaterThanOrEqualTo(1).WithMessage("Invalid Doctor Id");

        RuleFor(x => x.PatientId)
           .GreaterThanOrEqualTo(1).WithMessage("Invalid patient Id");
    }

    private bool ValidProperty(string? orderBy)
    {
        if (orderBy is null) return true;

        return PagingValidator.IsValidProperty(orderBy, typeof(PatientAppointmentsWithDoctorDetailsDto));
    }
}
