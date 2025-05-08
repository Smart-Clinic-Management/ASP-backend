namespace SmartClinic.Application.Features.Doctors.Query.GetDoctorWithSchedulesSlots;
public class GetDoctorWithSchedulesSlotsValidator : AbstractValidator<GetDoctorWithSchedulesSlotsParams>
{
    public GetDoctorWithSchedulesSlotsValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty();

        RuleFor(x => x.DoctorId)
            .NotEmpty();
    }
}
