public class UpdateSpecializationValidator : AbstractValidator<UpdateSpecializationRequest>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly int _specializationId; 

    public UpdateSpecializationValidator(IUnitOfWork unitOfWork, int specializationId)
    {
        _unitOfWork = unitOfWork;
        _specializationId = specializationId; 

        RuleFor(x => x.Name)
            .Length(3, 50)
            .When(x => !string.IsNullOrEmpty(x.Name))
            .MustAsync((name, token) => IsUniqueSpecializationName(name, _specializationId, token))
            .WithMessage("Specialization name already exists");

        RuleFor(x => x.Description)
            .Length(5, 200)
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Image)
            .Must(ValidImageExtension)
            .When(x => x.Image != null)
            .WithMessage("Only accept jpg|jpeg|png images")
            .Must(ValidateImageSize)
            .When(x => x.Image != null)
            .WithMessage("Image max size 2MB");
    }

    private async Task<bool> IsUniqueSpecializationName(string name, int specializationId, CancellationToken token)
    {
        var spec = new SpecializationByNameSpecification(name);
        var existingSpecialization = await _unitOfWork.Repo<Specialization>().GetEntityWithSpecAsync(spec);

        if (existingSpecialization != null && existingSpecialization.Id != specializationId)
        {
            return false;
        }

        return true;
    }

    private bool ValidImageExtension(IFormFile? file) => UserFormValidator.IsValidImageExtension(file!);

    private bool ValidateImageSize(IFormFile? file) => UserFormValidator.IsValidImageSize(file!);
}

