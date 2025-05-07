

namespace SmartClinic.Application.Features.Specializations.Query.GetSpecializations
{
    public record GetAllSpecializationsResponse(
      int Id,
      string Name,
      string Description,
      string Image
  );
}
