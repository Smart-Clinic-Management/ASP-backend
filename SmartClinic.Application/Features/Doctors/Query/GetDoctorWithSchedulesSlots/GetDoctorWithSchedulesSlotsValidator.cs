namespace SmartClinic.Application.Features.Doctors.Query.GetDoctorWithSchedulesSlots;
public class GetDoctorWithSchedulesSlotsValidator : AbstractValidator<GetDoctorWithSchedulesSlotsParams>
{
    public GetDoctorWithSchedulesSlotsValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .Matches(GlobalValidator.ValidDateRegex())
            .WithMessage("StartDate must be in format YYYY-MM-DD.")
            .Must(BeAValidDateOnly)
            .WithMessage("StartDate must be a valid calendar date.");

        RuleFor(x => x.DoctorId)
            .GreaterThanOrEqualTo(1)
            .NotEmpty();
    }

    private bool BeAValidDateOnly(string date) => GlobalValidator.BeAValidDateOnly(date);
}
