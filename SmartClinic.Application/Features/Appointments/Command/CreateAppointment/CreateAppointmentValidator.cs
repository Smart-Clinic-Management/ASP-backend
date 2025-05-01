using FluentValidation;

namespace SmartClinic.Application.Features.Appointments.Command.CreateAppointment;
public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.AppointmentDate)
            .NotEmpty()
            .Must(ValidDate).WithMessage("Not Valid Date");

        RuleFor(x => x.StartTime)
         .NotEmpty();


        RuleFor(x => x.DoctorId)
         .MustAsync(DoctorExists).WithMessage("Invalid Doctor Id")
         .NotEmpty();


        RuleFor(x => x.PatientId)
            .MustAsync(PatientExists).WithMessage("Invalid Patient Id")
            .NotEmpty();
        RuleFor(x => x.SpecializationId)
         .MustAsync(SpecializationExists).WithMessage("Invalid Specialization Id")
         .NotEmpty();


        this._unitOfWork = unitOfWork;
    }

    private async Task<bool> DoctorExists(int DoctorId, CancellationToken token)
         => await _unitOfWork.Repository<IDoctorRepository>().ExistsAsync(DoctorId);

    private async Task<bool> SpecializationExists(int specializationId, CancellationToken token)
         => await _unitOfWork.Repository<ISpecializaionRepository>().ExistsAsync(specializationId);

    private async Task<bool> PatientExists(int patientId, CancellationToken token)
        => await _unitOfWork.Repository<IPatient>().ExistsAsync(patientId);

    private bool ValidDate(DateOnly appointmentDate)
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        return appointmentDate >= currentDate;
    }


}
