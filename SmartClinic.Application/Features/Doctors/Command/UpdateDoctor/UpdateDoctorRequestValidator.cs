using FluentValidation;

namespace SmartClinic.Application.Features.Doctors.Command.UpdateDoctor;
public class UpdateDoctorRequestValidator : AbstractValidator<UpdateDoctorRequest>
{
    public UpdateDoctorRequestValidator()
    {
        RuleFor(x => x.Fname)
            .Length(3, 30);

        RuleFor(x => x.Lname)
            .Length(3, 30);

        RuleFor(x => x.BirthDate)
            .Must(ValidateMinimumAge).WithMessage("Minimum age is 25"); ;

        RuleFor(x => x.WaitingTime)
           .GreaterThanOrEqualTo(0)
           .LessThanOrEqualTo(30);

        RuleFor(x => x.PhoneNumber)
            .Must(ValidPhoneNumber)
            .WithMessage("Phone number must start with +20 and be exactly 13 characters long, including 10 digits after +20.");

        RuleFor(x => x.Address)
            .Length(3, 200);


        RuleFor(x => x.Description)
            .Length(3, 200);


        RuleFor(x => x.Image)
            .Must(ValidImageExtension)
            .WithMessage("Only accept jpg|jpeg|png images")
            .Must(ValidateImageSize).WithMessage("Image max size 2mb");

    }

    private bool ValidateImageSize(IFormFile? file)
    {
        if (file is null) return true;

        return UserFormValidator.IsValidImageSize(file);
    }

    private bool ValidImageExtension(IFormFile? file)
    {
        if (file is null) return true;

        return UserFormValidator.IsValidImageExtension(file);
    }

    private bool ValidPhoneNumber(string? PhoneNumber)
    {
        if (PhoneNumber is null) return true;
        return UserFormValidator.IsValidPhoneNumber(PhoneNumber);
    }

    private bool ValidateMinimumAge(DateOnly? birthDate)
    {
        if (birthDate is null) return true;

        return UserFormValidator.IsValidDoctorAge(birthDate);
    }
}
