using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Application.Features.Specializations.Command.DTOs.CreateSpecialization;
using SmartClinic.Application.Services.Implementation.Specifications.SpecializationSpecifications.CreateSpecializationsSpecification;
using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

namespace SmartClinic.Application.Features.Specializations.Command.CreateSpecialization;
public class CreateSpecializationValidator : AbstractValidator<CreateSpecializationRequest>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSpecializationValidator(IUnitOfWork unitOfWork)
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