using System.Reflection;
using FluentValidation;

namespace SmartClinic.Application.Features.Doctors.Query.GetDoctors;
public class GetAllDoctorsValidator : AbstractValidator<GetAllDoctorsParams>
{
    public GetAllDoctorsValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
           .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(20);

        RuleFor(x => x.OrderBy)
            .Must(ValidProperty).WithMessage("Invalid Property Name");
    }

    private bool ValidProperty(string? orderBy)
    {
        if (orderBy == null) return true;

        return typeof(GetAllDoctorsResponse).GetProperty(orderBy, BindingFlags.IgnoreCase
                | BindingFlags.Public | BindingFlags.Instance) is not null;
    }
}
