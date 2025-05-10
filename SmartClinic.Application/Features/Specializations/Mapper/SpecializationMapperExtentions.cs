namespace SmartClinic.Application.Features.Specializations.Mapper;

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

    public static GetSpecializationByIdResponse ToGetByIdDto(this Specialization specialization, IFileHandlerService fileHandler) => new(
        specialization.Id,
        specialization.Name,
        specialization.Description,
        fileHandler.GetFileURL(specialization.Image!),
        Enumerable.Empty<DoctorDto>()
    );



    public static bool UpdateEntity(this Specialization specialization, UpdateSpecializationRequest request)
    {
        bool isUpdated = false;

        if (!string.IsNullOrEmpty(request.Name) && specialization.Name != request.Name)
        {
            specialization.Name = request.Name;
            isUpdated = true;
        }

        if (!string.IsNullOrEmpty(request.Description) && specialization.Description != request.Description)
        {
            specialization.Description = request.Description;
            isUpdated = true;
        }

        if (request.Image != null)
        {
            specialization.Image = request.Image.ToRelativeFilePath();
            isUpdated = true;
        }

        return isUpdated;
    }


}
