namespace SmartClinic.Application.Features.Specializations.Query.GetSpecializations;

public class GetAllSpecializationsValidator : AbstractValidator<GetAllSpecializationsParams>
{
    public GetAllSpecializationsValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize).
            GreaterThanOrEqualTo(1).
            LessThanOrEqualTo(20);

    }
}
