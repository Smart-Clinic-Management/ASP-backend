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


        RuleFor(x => x.DoctorId)
         .MustAsync(DoctorExists).WithMessage("Invalid Doctor Id")
         .NotEmpty();

        RuleFor(x => x.SpecializationId)
         .NotEmpty();

        RuleFor(x => x)
        .MustAsync(IsValidDoctorSpecialization).WithMessage("Invalid Doctor Specialization Id");

        this._unitOfWork = unitOfWork;
    }


    private async Task<bool> IsValidDoctorSpecialization(CreateAppointmentDto dto, CancellationToken token)
    {
        var specs = new SpecializationByIdSpecification(dto.SpecializationId);
        return await _unitOfWork.Repo<Specialization>().ExistsWithSpecAsync(specs);
    }




    private async Task<bool> DoctorExists(int DoctorId, CancellationToken token)
    {
        var specs = new DoctorByIdSpecification(DoctorId);
        return await _unitOfWork.Repo<Doctor>().ExistsWithSpecAsync(specs);
    }

    private bool ValidDate(DateOnly appointmentDate)
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        return appointmentDate >= currentDate;
    }


}
