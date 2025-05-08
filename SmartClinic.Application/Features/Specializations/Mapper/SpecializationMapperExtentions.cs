using SmartClinic.Application.Features.Specializations.Command.DTOs.CreateSpecialization;

namespace SmartClinic.Application.Features.Specializations.Mapper
{
    public static class SpecializationMapperExtensions
    {
        public static Specialization ToSpecialization(this CreateSpecializationRequest request)
        {
            return new Specialization
            {
                Name = request.Name,
                Description = request.Description,
                Image = request.Image?.ToRelativeFilePath()
            };
        }

    }
}
