
using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

namespace SmartClinic.Application.Features.Doctors.Command.CreateDoctor;
public class CreateDoctorValidator : AbstractValidator<CreateDoctorRequest>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;

    public CreateDoctorValidator(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
           .Length(3, 30);

        RuleFor(x => x.SpecializationId)
            .NotEmpty()
            .MustAsync(IsValidSpecificaionId).WithMessage("Invalid specialization Id");

        RuleFor(x => x.LastName)
            .Length(3, 30);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(IsUniqueEmail).WithMessage("Email already registered");

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .Must(ValidateMinimumAge).WithMessage("Minimum age is 25"); ;

        RuleFor(x => x.WaitingTime)
           .GreaterThanOrEqualTo(0)
           .LessThanOrEqualTo(30);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Must(ValidPhoneNumber)
            .WithMessage("Phone number must start with +20 and be exactly 13 characters long, including 10 digits after +20.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .Length(3, 200);


        RuleFor(x => x.Description)
            .Length(3, 200);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Must(ValidPassword);


        RuleFor(x => x.Image)
            .NotEmpty()
            .Must(ValidImageExtension)
            .WithMessage("Only accept jpg|jpeg|png images")
            .Must(ValidateImageSize).WithMessage("Image max size 2mb");


        this._unitOfWork = unitOfWork;
        this._userManager = userManager;
    }

    private bool ValidPassword(string Pass)
    {
        string pattern = @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}";

        return Regex.IsMatch(pattern, Pass);
    }

    private async Task<bool> IsUniqueEmail(string Email, CancellationToken token) =>
        !await _userManager.Users.AnyAsync(u => u.Email == Email);

    private async Task<bool> IsValidSpecificaionId(int SpecificaionId, CancellationToken token)
        => await _unitOfWork.Repo<Specialization>().Exists(SpecificaionId);

    private bool ValidateImageSize(IFormFile? file) => UserFormValidator.IsValidImageSize(file!);

    private bool ValidImageExtension(IFormFile? file) => UserFormValidator.IsValidImageExtension(file!);

    private bool ValidPhoneNumber(string PhoneNumber) => UserFormValidator.IsValidPhoneNumber(PhoneNumber);

    private bool ValidateMinimumAge(DateOnly birthDate) => UserFormValidator.IsValidDoctorAge(birthDate);
}
