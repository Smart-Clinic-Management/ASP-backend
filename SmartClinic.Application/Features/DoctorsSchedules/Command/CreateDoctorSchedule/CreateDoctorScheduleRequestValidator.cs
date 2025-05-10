namespace SmartClinic.Application.Features.DoctorsSchedules.Command.CreateDoctorSchedule;
public class CreateDoctorScheduleRequestValidator : AbstractValidator<CreateDoctorScheduleRequest>
{
    public IUnitOfWork UnitOfWork { get; }
    public CreateDoctorScheduleRequestValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x)
            .NotEmpty()
            .Must(ValidDuration).WithMessage("End time must be greater than and at least 2 hours greater");

        RuleFor(x => Enum.IsDefined<DayOfWeek>(x.DayOfWeek))
            .NotEmpty();

        RuleFor(x => x.SlotDuration)
            .NotEmpty()
            .LessThanOrEqualTo(60)
            .GreaterThanOrEqualTo(5);

        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .MustAsync(IsValidDoctorId).WithMessage("Invalid doctor Id")
            .GreaterThanOrEqualTo(1);


        UnitOfWork = unitOfWork;
    }


    private async Task<bool> IsValidDoctorId(int DoctorId, CancellationToken token)
    {
        var specs = new DoctorByIdSpecification(DoctorId);
        return await UnitOfWork.Repo<Doctor>().ExistsWithSpecAsync(specs);
    }

    private bool ValidDuration(CreateDoctorScheduleRequest request)
    {
        return request.EndTime >= request.StartTime.AddHours(2);
    }
}
