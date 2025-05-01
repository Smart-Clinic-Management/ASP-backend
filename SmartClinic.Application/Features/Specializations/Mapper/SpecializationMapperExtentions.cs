using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartClinic.Application.Features.Specializations.Command.DTOs.CreateSpecialization;
using SmartClinic.Application.Features.Specializations.Command.DTOs.UpdateSpecialization;

namespace SmartClinic.Application.Features.Specializations.Mapper
{
    public static class SpecializationMapperExtentions
    {
        public static Specialization ToSpecialization(this CreateSpecializationRequest request, string imageUrl, string userId)
        {
            return new Specialization
            {
                Name = request.Name,
                Description = request.Description,
                Image = imageUrl,

            };
        }
        public static string GetImgUrl(string? path, IHttpContextAccessor _httpContextAccessor)
        {
            if (path == null) return null!;

            var request = _httpContextAccessor.HttpContext?.Request;
            return $"{request!.Scheme}://{request.Host}/{path.Replace("\\", "/")}";
        }

        public static void UpdateSpecializationFromRequest(this Specialization specialization, UpdateSpecializationRequest request)
        {
            specialization.Name = request.Name ?? specialization.Name;
            specialization.Description = request.Description ?? specialization.Description;
            specialization.Image = request.Image ?? specialization.Image;
            specialization.IsActive = request.IsActive ?? specialization.IsActive;
        }
    }
}
