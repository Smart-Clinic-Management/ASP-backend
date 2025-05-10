using SmartClinic.Application.Features.Specializations.Command.UpdateSpecialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UpdateSpecializationValidator : AbstractValidator<UpdateSpecializationRequest>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSpecializationValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 50)
            .MustAsync(IsUniqueSpecializationName).WithMessage("Specialization name already exists");

        RuleFor(x => x.Description)
            .NotEmpty()
            .Length(5, 200);

        RuleFor(x => x.Image)
            .NotEmpty()
            .Must(ValidImageExtension)
            .WithMessage("Only accept jpg|jpeg|png images")
            .Must(ValidateImageSize).WithMessage("Image max size 2MB");
    }

    private async Task<bool> IsUniqueSpecializationName(string name, CancellationToken token)
    {
        var spec = new SpecializationByNameSpecification(name);
        return !await _unitOfWork.Repo<Specialization>().ExistsWithSpecAsync(spec);
    }


    private bool ValidImageExtension(IFormFile? file) => UserFormValidator.IsValidImageExtension(file!);

    private bool ValidateImageSize(IFormFile? file) => UserFormValidator.IsValidImageSize(file!);

}