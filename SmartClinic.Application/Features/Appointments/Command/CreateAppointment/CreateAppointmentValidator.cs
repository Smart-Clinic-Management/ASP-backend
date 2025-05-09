namespace SmartClinic.Application.Features.Appointments.Command.CreateAppointment;
public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentRequest>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.AppointmentDate)
            .Matches(GlobalValidator.ValidDateRegex())
            .WithMessage("AppointmentDate must be in format YYYY-MM-DD.")
            .Must(ValidDate)
            .WithMessage("AppointmentDate must be a valid calendar date , date must be at least today.")
            .NotEmpty();

        RuleFor(x => x.StartTime)
         .NotEmpty();


        RuleFor(x => x.DoctorId)
         .MustAsync(DoctorExists).WithMessage("Invalid Doctor Id")
         .NotEmpty();

        RuleFor(x => x.SpecializationId)
         .NotEmpty();

        RuleFor(x => x)
        .MustAsync(IsValidDoctorSpecialization).WithMessage("Invalid Doctor Specialization Id");

        this._unitOfWork = unitOfWork;
    }


    private async Task<bool> IsValidDoctorSpecialization(CreateAppointmentRequest dto, CancellationToken token)
    {
        var specs = new SpecializationByIdSpecification(dto.SpecializationId);
        return await _unitOfWork.Repo<Specialization>().ExistsWithSpecAsync(specs);
    }




    private async Task<bool> DoctorExists(int DoctorId, CancellationToken token)
    {
        var specs = new DoctorByIdSpecification(DoctorId);
        return await _unitOfWork.Repo<Doctor>().ExistsWithSpecAsync(specs);
    }

    private bool ValidDate(string appointmentDate)
    {
        if (GlobalValidator.BeAValidDateOnly(appointmentDate))
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);

            return appointmentDate.ToDate() >= currentDate;
        }
        return false;
    }

}
