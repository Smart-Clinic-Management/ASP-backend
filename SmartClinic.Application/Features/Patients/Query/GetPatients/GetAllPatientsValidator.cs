﻿namespace SmartClinic.Application.Features.Patients.Query.GetPatients;

public class GetAllPatientsValidator : AbstractValidator<GetAllPatientsParams>
{
    public GetAllPatientsValidator()
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
        if (orderBy is null) return true;

        return PagingValidator.IsValidProperty(orderBy, typeof(GetAllPatientsResponse));
    }
}
